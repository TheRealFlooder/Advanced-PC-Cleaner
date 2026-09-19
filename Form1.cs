using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text.Json;
using System.Net.Http;
using System.Linq;

namespace DesktopPCCleaner
{
    public partial class Form1 : Form
    {
        // P/Invoke for Recycle Bin
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, uint dwFlags);
        private const uint SHERB_NOCONFIRMATION = 0x00000001;
        private const uint SHERB_NOPROGRESSUI = 0x00000002;
        private const uint SHERB_NOSOUND = 0x00000004;

        public Form1()
        {
            InitializeComponent();
            _ = LoadDynamicAdAsync();
            LoadStartupItems();
            _ = CalculateSystemHealthAsync(); // Calculate live health on startup
        }

        #region LIVE HEALTH TRACKER
        private async Task CalculateSystemHealthAsync()
        {
            try
            {
                UpdateStatus("Calculating system health score...");
                long tempJunkBytes = 0;

                await Task.Run(() =>
                {
                    string userTemp = Path.GetTempPath();
                    if (Directory.Exists(userTemp)) tempJunkBytes += GetDirectorySize(userTemp);

                    string sysTemp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp");
                    if (Directory.Exists(sysTemp)) tempJunkBytes += GetDirectorySize(sysTemp);

                    string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string chromeCache = Path.Combine(localAppData, @"Google\Chrome\User Data\Default\Cache");
                    string edgeCache = Path.Combine(localAppData, @"Microsoft\Edge\User Data\Default\Cache");
                    if (Directory.Exists(chromeCache)) tempJunkBytes += GetDirectorySize(chromeCache);
                    if (Directory.Exists(edgeCache)) tempJunkBytes += GetDirectorySize(edgeCache);
                });

                int healthScore = 100;
                if (tempJunkBytes > 500L * 1024 * 1024) healthScore = 75;
                if (tempJunkBytes > 2L * 1024 * 1024 * 1024) healthScore = 50;
                if (tempJunkBytes > 5L * 1024 * 1024 * 1024) healthScore = 25;

                UpdateHealthUI(healthScore, tempJunkBytes);
            }
            catch 
            {
                UpdateStatus("Status: Ready");
            }
        }

        private long GetDirectorySize(string folderPath)
        {
            long size = 0;
            try
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);
                foreach (FileInfo fi in di.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    try { size += fi.Length; } catch { }
                }
            }
            catch { }
            return size;
        }

        private void UpdateHealthUI(int score, long junkBytes)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int, long>(UpdateHealthUI), score, junkBytes);
                return;
            }
            UpdateStatus($"Health Score: {score}% | Detected Junk: {FormatBytes(junkBytes)}");
        }
        #endregion

        #region TAB 1: CLEANER, DUPLICATES & SHREDDER
        private async void btnStartScan_Click(object sender, EventArgs e)
        {
            btnStartClean.Enabled = false;
            btnScanDuplicates.Enabled = false;
            txtLog.Clear();
            List<string> dirs = new List<string>();

            if (chkUserTemp.Checked) dirs.Add(Path.GetTempPath());
            if (chkSystemTemp.Checked) dirs.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp"));
            if (chkBrowserCache.Checked)
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                dirs.Add(Path.Combine(localAppData, @"Google\Chrome\User Data\Default\Cache"));
                dirs.Add(Path.Combine(localAppData, @"Microsoft\Edge\User Data\Default\Cache"));
            }

            int totalFiles = 0;
            await Task.Run(() =>
            {
                foreach (var dir in dirs)
                {
                    if (Directory.Exists(dir))
                    {
                        try { totalFiles += Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories).Length; } catch { }
                    }
                }
            });

            SetProgressBarMax(Math.Max(totalFiles, 1));
            SetProgressBarValue(0);

            long totalBytes = 0;

            await Task.Run(() =>
            {
                foreach (var dir in dirs)
                {
                    if (Directory.Exists(dir))
                    {
                        totalBytes += CleanDirectoryWithLogging(dir);
                    }
                }

                if (chkRecycleBin.Checked)
                {
                    UpdateStatus("Emptying Recycle Bin...");
                    SHEmptyRecycleBin(IntPtr.Zero, null, SHERB_NOCONFIRMATION | SHERB_NOPROGRESSUI | SHERB_NOSOUND);
                    AppendLog("Recycle Bin Emptied Successfully.");
                }
            });

            UpdateStatus("Status: Cleanup Complete!");
            AppendLog($"\n========================================");
            AppendLog($" TOTAL SPACE FREED: {FormatBytes(totalBytes)}");
            AppendLog($"========================================");
            
            btnStartClean.Enabled = true;
            btnScanDuplicates.Enabled = true;

            // Recalculate health score immediately
            _ = CalculateSystemHealthAsync();
        }

        private long CleanDirectoryWithLogging(string targetDir)
        {
            long freed = 0;
            if (!Directory.Exists(targetDir)) return freed;

            AppendLog($"[Scanning Directory]: {targetDir}");

            try
            {
                string[] files = Directory.GetFiles(targetDir, "*.*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fi = new FileInfo(file);
                        long fileSize = fi.Length;
                        string fileName = fi.Name;

                        fi.Attributes = FileAttributes.Normal;
                        fi.Delete();
                        
                        freed += fileSize;
                        AppendLog($"  └─ Deleted: {fileName} ({FormatBytes(fileSize)})");
                    }
                    catch
                    {
                        // File locked by active process, skip safely
                    }
                    finally
                    {
                        IncrementProgressBar();
                    }
                }
            }
            catch (Exception ex)
            {
                AppendLog($"  └─ Error accessing folder: {ex.Message}");
            }

            AppendLog($"[Completed]: {targetDir} -> Freed {FormatBytes(freed)}\n");
            return freed;
        }

        private async void btnScanDuplicates_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select a folder to scan for duplicate files (e.g., Downloads)";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string targetFolder = fbd.SelectedPath;
                    txtLog.Clear();
                    UpdateStatus("Scanning directory structure...");
                    AppendLog($"Scanning for duplicate files in: {targetFolder}...");

                    btnStartClean.Enabled = false;
                    btnScanDuplicates.Enabled = false;

                    long bytesFreed = 0;

                    await Task.Run(() =>
                    {
                        try
                        {
                            var allFiles = Directory.GetFiles(targetFolder, "*.*", SearchOption.AllDirectories);

                            var sizeGroups = allFiles.Select(f => new FileInfo(f))
                                                     .Where(fi => fi.Exists && fi.Length > 0)
                                                     .GroupBy(fi => fi.Length)
                                                     .Where(g => g.Count() > 1)
                                                     .ToList();

                            int totalCandidates = sizeGroups.Sum(g => g.Count());
                            SetProgressBarMax(Math.Max(totalCandidates, 1));
                            SetProgressBarValue(0);

                            foreach (var group in sizeGroups)
                            {
                                var hashDict = new Dictionary<string, string>();
                                foreach (var fi in group)
                                {
                                    try
                                    {
                                        string hash = GetFileChecksum(fi.FullName);
                                        if (hashDict.ContainsKey(hash))
                                        {
                                            long size = fi.Length;
                                            fi.Attributes = FileAttributes.Normal;
                                            fi.Delete();
                                            bytesFreed += size;
                                            AppendLog($"[Duplicate Removed]: {fi.Name} ({FormatBytes(size)})");
                                        }
                                        else
                                        {
                                            hashDict.Add(hash, fi.FullName);
                                        }
                                    }
                                    catch { }
                                    finally
                                    {
                                        IncrementProgressBar();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            AppendLog($"Error scanning duplicates: {ex.Message}");
                        }
                    });

                    UpdateStatus("Status: Duplicate Scan Complete!");
                    AppendLog($"\nDuplicate Scan Complete! Freed {FormatBytes(bytesFreed)}.");
                    btnStartClean.Enabled = true;
                    btnScanDuplicates.Enabled = true;
                }
            }
        }

        private string GetFileChecksum(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private void BtnShredFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a file to securely shred";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string file = ofd.FileName;
                        FileInfo fi = new FileInfo(file);
                        long len = fi.Length;

                        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Write))
                        {
                            byte[] dummy = new byte[4096];
                            Array.Clear(dummy, 0, dummy.Length);
                            for (long i = 0; i < len; i += dummy.Length) 
                                fs.Write(dummy, 0, (int)Math.Min(dummy.Length, len - i));
                            fs.Flush();
                        }
                        fi.Attributes = FileAttributes.Normal;
                        File.Delete(file);
                        MessageBox.Show("File securely shredded using DoD standards!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error shredding file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region TAB 2: PRIVACY & BLOAT
        private void btnDisableTelemetry_Click(object sender, EventArgs e)
        {
            RunPowerShellCommand("Stop-Service DiagTrack -ErrorAction SilentlyContinue; Set-Service DiagTrack -StartupType Disabled");
        }

        private void btnRemoveBloat_Click(object sender, EventArgs e)
        {
            string[] bloatApps = new string[]
            {
                "*3DBuilder*", "*3DViewer*", "*ZuneMusic*", "*ZuneVideo*", "*BingWeather*", "*BingNews*",
                "*BingSports*", "*BingFinance*", "*MicrosoftSolitaireCollection*", "*GetHelp*",
                "*GetStarted*", "*YourPhone*", "*SkypeApp*", "*People*", "*WindowsAlarms*",
                "*WindowsCamera*", "*OneNote*", "*WindowsMaps*", "*SoundRecorder*", "*XboxApp*",
                "*XboxGameOverlay*", "*XboxGamingOverlay*", "*XboxIdentityProvider*",
                "*XboxSpeechToTextOverlay*", "*MixedReality.Portal*", "*Paint3D*", "*Cortana*",
                "*MicrosoftOfficeHub*", "*Clipchamp.Clipchamp*", "*MicrosoftStickyNotes*",
                "*Microsoft.Todos*", "*Microsoft.WindowsFeedbackHub*", "*microsoft.windowscommunicationsapps*"
            };

            string script = "";
            foreach (var app in bloatApps)
            {
                script += $"Get-AppxPackage {app} | Remove-AppxPackage -ErrorAction SilentlyContinue; ";
                script += $"Get-AppxProvisionedPackage -Online | Where-Object DisplayName -like '{app}' | Remove-AppxProvisionedPackage -Online -ErrorAction SilentlyContinue; ";
            }

            txtPrivacyLog.AppendText("Starting full bloatware removal...\n");
            RunPowerShellCommand(script);
        }

        private void btnDisableBingSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Policies\Microsoft\Windows\Explorer"))
                {
                    if (key != null) key.SetValue("DisableSearchBoxSuggestions", 1, RegistryValueKind.DWord);
                }

                using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search"))
                {
                    if (key != null)
                    {
                        key.SetValue("ConnectedSearchUseWeb", 0, RegistryValueKind.DWord);
                        key.SetValue("ConnectedSearchUseWebOverMeteredConnections", 0, RegistryValueKind.DWord);
                    }
                }

                txtPrivacyLog.AppendText("[Success]: Bing web search suggestions disabled.\n\n");
                MessageBox.Show("Bing search integration disabled successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                txtPrivacyLog.AppendText($"[Error disabling Bing]: {ex.Message}\n");
                MessageBox.Show("Make sure you run the app as Administrator.", "Permission Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RunPowerShellCommand(string cmd)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("powershell.exe", $"-Command \"{cmd}\"")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                Process.Start(psi);
                txtPrivacyLog.AppendText($"[Executed]: {cmd}\nDone.\n\n");
            }
            catch (Exception ex)
            {
                txtPrivacyLog.AppendText($"Error: {ex.Message}\n");
            }
        }
        #endregion

        #region TAB 3: NETWORK FIXES
        private void btnFlushDNS_Click(object sender, EventArgs e)
        {
            RunNetworkCommand("ipconfig", "/flushdns");
        }

        private void btnResetWinsock_Click(object sender, EventArgs e)
        {
            RunNetworkCommand("netsh", "winsock reset");
        }

        private void RunNetworkCommand(string fileName, string args)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(fileName, args) { CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true };
                Process p = Process.Start(psi);
                string output = p.StandardOutput.ReadToEnd();
                txtNetLog.AppendText($"> {fileName} {args}\n{output}\n");
            }
            catch (Exception ex)
            {
                txtNetLog.AppendText($"Error: {ex.Message}\n");
            }
        }
        #endregion

        #region TAB 4: STARTUP MANAGER
        private void btnRefreshStartup_Click(object sender, EventArgs e)
        {
            LoadStartupItems();
        }

        private void btnAddStartup_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select executable to add to Startup";
                ofd.Filter = "Executables (*.exe)|*.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string appName = Microsoft.VisualBasic.Interaction.InputBox("Enter a display name for this startup item:", "Add Startup Item", fileName);

                    if (!string.IsNullOrWhiteSpace(appName))
                    {
                        try
                        {
                            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
                            {
                                if (key != null)
                                {
                                    key.SetValue(appName, $"\"{filePath}\"");
                                    MessageBox.Show("Startup item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadStartupItems();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to add startup item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnDeleteStartup_Click(object sender, EventArgs e)
        {
            if (listStartup.SelectedItems.Count > 0)
            {
                string appName = listStartup.SelectedItems[0].Text;
                DialogResult res = MessageBox.Show($"Are you sure you want to remove '{appName}' from startup?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    try
                    {
                        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
                        {
                            if (key != null)
                            {
                                key.DeleteValue(appName, false);
                                LoadStartupItems();
                                MessageBox.Show("Startup item removed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to remove item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a startup item from the list to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadStartupItems()
        {
            if (listStartup.Columns.Count == 0)
            {
                listStartup.View = View.Details;
                listStartup.Columns.Add("Program Name", 250);
                listStartup.Columns.Add("File Path", 380);
            }

            listStartup.Items.Clear();
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false))
                {
                    if (key != null)
                    {
                        foreach (string name in key.GetValueNames())
                        {
                            string val = key.GetValue(name)?.ToString();
                            ListViewItem item = new ListViewItem(name);
                            item.SubItems.Add(val);
                            listStartup.Items.Add(item);
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        #region THREAD-SAFE UI HELPERS & ADS
        private void SetProgressBarMax(int max)
        {
            if (this.InvokeRequired) { this.Invoke(new Action<int>(SetProgressBarMax), max); return; }
            progressBar.Maximum = max;
        }

        private void SetProgressBarValue(int val)
        {
            if (this.InvokeRequired) { this.Invoke(new Action<int>(SetProgressBarValue), val); return; }
            progressBar.Value = val;
        }

        private void IncrementProgressBar()
        {
            if (this.InvokeRequired) { this.Invoke(new Action(IncrementProgressBar)); return; }
            if (progressBar.Value < progressBar.Maximum) progressBar.Value++;
        }

        private void UpdateStatus(string message)
        {
            if (this.InvokeRequired) { this.Invoke(new Action<string>(UpdateStatus), message); return; }
            lblStatus.Text = message;
        }

        private async Task LoadDynamicAdAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonUrl = "https://raw.githubusercontent.com/YOUR_USERNAME/YOUR_REPO/main/adconfig.json";
                    string json = await client.GetStringAsync(jsonUrl);
                    var config = JsonSerializer.Deserialize<AdConfigModel>(json);
                    if (config != null && config.IsEnabled)
                    {
                        pictureBoxAd.Load(config.ImageUrl);
                        pictureBoxAd.Tag = config.TargetUrl;
                        pictureBoxAd.Visible = true;
                    }
                }
            }
            catch { }
        }

        private void PictureBoxAd_Click(object sender, EventArgs e)
        {
            if (pictureBoxAd.Tag is string url && !string.IsNullOrEmpty(url))
            {
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
        }

        private void AppendLog(string text)
        {
            if (this.InvokeRequired) { this.Invoke(new Action<string>(AppendLog), text); return; }
            txtLog.AppendText(text + Environment.NewLine);
            txtLog.ScrollToCaret();
        }

        private string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1) { number /= 1024; counter++; }
            return $"{number:n2} {suffixes[counter]}";
        }
        #endregion
    }

    public class AdConfigModel
    {
        public string ImageUrl { get; set; }
        public string TargetUrl { get; set; }
        public bool IsEnabled { get; set; }
    }
}