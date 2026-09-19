namespace DesktopPCCleaner
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tabControl = new TabControl();
            tabCleaner = new TabPage();
            txtLog = new RichTextBox();
            lblStatus = new Label();
            progressBar = new ProgressBar();
            btnShredFile = new Button();
            btnScanDuplicates = new Button();
            btnStartClean = new Button();
            chkRecycleBin = new CheckBox();
            chkBrowserCache = new CheckBox();
            chkSystemTemp = new CheckBox();
            chkUserTemp = new CheckBox();
            tabPrivacy = new TabPage();
            txtPrivacyLog = new RichTextBox();
            btnDisableBing = new Button();
            btnRemoveBloat = new Button();
            btnDisableTelemetry = new Button();
            tabNetwork = new TabPage();
            txtNetLog = new RichTextBox();
            btnResetWinsock = new Button();
            btnFlushDNS = new Button();
            tabStartup = new TabPage();
            listStartup = new ListView();
            btnDeleteStartup = new Button();
            btnAddStartup = new Button();
            btnRefreshStartup = new Button();
            pictureBoxAd = new PictureBox();
            tabControl.SuspendLayout();
            tabCleaner.SuspendLayout();
            tabPrivacy.SuspendLayout();
            tabNetwork.SuspendLayout();
            tabStartup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAd).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabCleaner);
            tabControl.Controls.Add(tabPrivacy);
            tabControl.Controls.Add(tabNetwork);
            tabControl.Controls.Add(tabStartup);
            tabControl.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            tabControl.Location = new Point(12, 12);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(710, 470);
            tabControl.TabIndex = 0;
            // 
            // tabCleaner
            // 
            tabCleaner.BackColor = Color.White;
            tabCleaner.Controls.Add(txtLog);
            tabCleaner.Controls.Add(lblStatus);
            tabCleaner.Controls.Add(progressBar);
            tabCleaner.Controls.Add(btnShredFile);
            tabCleaner.Controls.Add(btnScanDuplicates);
            tabCleaner.Controls.Add(btnStartClean);
            tabCleaner.Controls.Add(chkRecycleBin);
            tabCleaner.Controls.Add(chkBrowserCache);
            tabCleaner.Controls.Add(chkSystemTemp);
            tabCleaner.Controls.Add(chkUserTemp);
            tabCleaner.Location = new Point(4, 26);
            tabCleaner.Name = "tabCleaner";
            tabCleaner.Padding = new Padding(3);
            tabCleaner.Size = new Size(702, 440);
            tabCleaner.TabIndex = 0;
            tabCleaner.Text = "Cleaner & Duplicates";
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.FromArgb(248, 250, 252);
            txtLog.BorderStyle = BorderStyle.None;
            txtLog.Font = new Font("Consolas", 9.5F);
            txtLog.ForeColor = Color.FromArgb(15, 23, 42);
            txtLog.Location = new Point(20, 215);
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.Size = new Size(660, 205);
            txtLog.TabIndex = 9;
            txtLog.Text = "";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(14, 165, 233);
            lblStatus.Location = new Point(16, 160);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(99, 19);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status: Ready";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(20, 190);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(660, 8);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 7;
            // 
            // btnShredFile
            // 
            btnShredFile.BackColor = Color.FromArgb(239, 68, 68);
            btnShredFile.Cursor = Cursors.Hand;
            btnShredFile.FlatAppearance.BorderSize = 0;
            btnShredFile.FlatStyle = FlatStyle.Flat;
            btnShredFile.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnShredFile.ForeColor = Color.White;
            btnShredFile.Location = new Point(560, 20);
            btnShredFile.Name = "btnShredFile";
            btnShredFile.Size = new Size(120, 40);
            btnShredFile.TabIndex = 6;
            btnShredFile.Text = "Secure Shred";
            btnShredFile.UseVisualStyleBackColor = false;
            btnShredFile.Click += BtnShredFile_Click;
            // 
            // btnScanDuplicates
            // 
            btnScanDuplicates.BackColor = Color.FromArgb(224, 242, 254);
            btnScanDuplicates.Cursor = Cursors.Hand;
            btnScanDuplicates.FlatAppearance.BorderSize = 0;
            btnScanDuplicates.FlatStyle = FlatStyle.Flat;
            btnScanDuplicates.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnScanDuplicates.ForeColor = Color.FromArgb(3, 105, 161);
            btnScanDuplicates.Location = new Point(415, 20);
            btnScanDuplicates.Name = "btnScanDuplicates";
            btnScanDuplicates.Size = new Size(135, 40);
            btnScanDuplicates.TabIndex = 5;
            btnScanDuplicates.Text = "Scan Duplicates";
            btnScanDuplicates.UseVisualStyleBackColor = false;
            btnScanDuplicates.Click += btnScanDuplicates_Click;
            // 
            // btnStartClean
            // 
            btnStartClean.BackColor = Color.FromArgb(14, 165, 233);
            btnStartClean.Cursor = Cursors.Hand;
            btnStartClean.FlatAppearance.BorderSize = 0;
            btnStartClean.FlatStyle = FlatStyle.Flat;
            btnStartClean.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnStartClean.ForeColor = Color.White;
            btnStartClean.Location = new Point(290, 20);
            btnStartClean.Name = "btnStartClean";
            btnStartClean.Size = new Size(115, 40);
            btnStartClean.TabIndex = 4;
            btnStartClean.Text = "Start Cleaning";
            btnStartClean.UseVisualStyleBackColor = false;
            btnStartClean.Click += btnStartScan_Click;
            // 
            // chkRecycleBin
            // 
            chkRecycleBin.AutoSize = true;
            chkRecycleBin.Checked = true;
            chkRecycleBin.CheckState = CheckState.Checked;
            chkRecycleBin.Font = new Font("Segoe UI", 10F);
            chkRecycleBin.ForeColor = Color.FromArgb(51, 65, 85);
            chkRecycleBin.Location = new Point(20, 110);
            chkRecycleBin.Name = "chkRecycleBin";
            chkRecycleBin.Size = new Size(198, 23);
            chkRecycleBin.TabIndex = 3;
            chkRecycleBin.Text = "Empty Windows Recycle Bin";
            chkRecycleBin.UseVisualStyleBackColor = true;
            // 
            // chkBrowserCache
            // 
            chkBrowserCache.AutoSize = true;
            chkBrowserCache.Checked = true;
            chkBrowserCache.CheckState = CheckState.Checked;
            chkBrowserCache.Font = new Font("Segoe UI", 10F);
            chkBrowserCache.ForeColor = Color.FromArgb(51, 65, 85);
            chkBrowserCache.Location = new Point(20, 80);
            chkBrowserCache.Name = "chkBrowserCache";
            chkBrowserCache.Size = new Size(266, 23);
            chkBrowserCache.TabIndex = 2;
            chkBrowserCache.Text = "Clean Browser Caches (Chrome / Edge)";
            chkBrowserCache.UseVisualStyleBackColor = true;
            // 
            // chkSystemTemp
            // 
            chkSystemTemp.AutoSize = true;
            chkSystemTemp.Checked = true;
            chkSystemTemp.CheckState = CheckState.Checked;
            chkSystemTemp.Font = new Font("Segoe UI", 10F);
            chkSystemTemp.ForeColor = Color.FromArgb(51, 65, 85);
            chkSystemTemp.Location = new Point(20, 50);
            chkSystemTemp.Name = "chkSystemTemp";
            chkSystemTemp.Size = new Size(261, 23);
            chkSystemTemp.TabIndex = 1;
            chkSystemTemp.Text = "Clean System Temporary Files (Admin)";
            chkSystemTemp.UseVisualStyleBackColor = true;
            // 
            // chkUserTemp
            // 
            chkUserTemp.AutoSize = true;
            chkUserTemp.Checked = true;
            chkUserTemp.CheckState = CheckState.Checked;
            chkUserTemp.Font = new Font("Segoe UI", 10F);
            chkUserTemp.ForeColor = Color.FromArgb(51, 65, 85);
            chkUserTemp.Location = new Point(20, 20);
            chkUserTemp.Name = "chkUserTemp";
            chkUserTemp.Size = new Size(193, 23);
            chkUserTemp.TabIndex = 0;
            chkUserTemp.Text = "Clean User Temporary Files";
            chkUserTemp.UseVisualStyleBackColor = true;
            // 
            // tabPrivacy
            // 
            tabPrivacy.BackColor = Color.White;
            tabPrivacy.Controls.Add(txtPrivacyLog);
            tabPrivacy.Controls.Add(btnDisableBing);
            tabPrivacy.Controls.Add(btnRemoveBloat);
            tabPrivacy.Controls.Add(btnDisableTelemetry);
            tabPrivacy.Location = new Point(4, 26);
            tabPrivacy.Name = "tabPrivacy";
            tabPrivacy.Padding = new Padding(3);
            tabPrivacy.Size = new Size(702, 440);
            tabPrivacy.TabIndex = 1;
            tabPrivacy.Text = "Privacy & Bloat";
            // 
            // txtPrivacyLog
            // 
            txtPrivacyLog.BackColor = Color.FromArgb(248, 250, 252);
            txtPrivacyLog.BorderStyle = BorderStyle.None;
            txtPrivacyLog.Font = new Font("Consolas", 9.5F);
            txtPrivacyLog.ForeColor = Color.FromArgb(15, 23, 42);
            txtPrivacyLog.Location = new Point(310, 25);
            txtPrivacyLog.Name = "txtPrivacyLog";
            txtPrivacyLog.ReadOnly = true;
            txtPrivacyLog.Size = new Size(370, 395);
            txtPrivacyLog.TabIndex = 3;
            txtPrivacyLog.Text = "";
            // 
            // btnDisableBing
            // 
            btnDisableBing.BackColor = Color.FromArgb(240, 249, 255);
            btnDisableBing.Cursor = Cursors.Hand;
            btnDisableBing.FlatAppearance.BorderColor = Color.FromArgb(186, 230, 253);
            btnDisableBing.FlatStyle = FlatStyle.Flat;
            btnDisableBing.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnDisableBing.ForeColor = Color.FromArgb(3, 105, 161);
            btnDisableBing.Location = new Point(20, 145);
            btnDisableBing.Name = "btnDisableBing";
            btnDisableBing.Size = new Size(270, 45);
            btnDisableBing.TabIndex = 2;
            btnDisableBing.Text = "Disable Bing Web Search in Start Menu";
            btnDisableBing.UseVisualStyleBackColor = false;
            btnDisableBing.Click += btnDisableBingSearch_Click;
            // 
            // btnRemoveBloat
            // 
            btnRemoveBloat.BackColor = Color.FromArgb(240, 249, 255);
            btnRemoveBloat.Cursor = Cursors.Hand;
            btnRemoveBloat.FlatAppearance.BorderColor = Color.FromArgb(186, 230, 253);
            btnRemoveBloat.FlatStyle = FlatStyle.Flat;
            btnRemoveBloat.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnRemoveBloat.ForeColor = Color.FromArgb(3, 105, 161);
            btnRemoveBloat.Location = new Point(20, 85);
            btnRemoveBloat.Name = "btnRemoveBloat";
            btnRemoveBloat.Size = new Size(270, 45);
            btnRemoveBloat.TabIndex = 1;
            btnRemoveBloat.Text = "Remove All Windows Bloat";
            btnRemoveBloat.UseVisualStyleBackColor = false;
            btnRemoveBloat.Click += btnRemoveBloat_Click;
            // 
            // btnDisableTelemetry
            // 
            btnDisableTelemetry.BackColor = Color.FromArgb(240, 249, 255);
            btnDisableTelemetry.Cursor = Cursors.Hand;
            btnDisableTelemetry.FlatAppearance.BorderColor = Color.FromArgb(186, 230, 253);
            btnDisableTelemetry.FlatStyle = FlatStyle.Flat;
            btnDisableTelemetry.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnDisableTelemetry.ForeColor = Color.FromArgb(3, 105, 161);
            btnDisableTelemetry.Location = new Point(20, 25);
            btnDisableTelemetry.Name = "btnDisableTelemetry";
            btnDisableTelemetry.Size = new Size(270, 45);
            btnDisableTelemetry.TabIndex = 0;
            btnDisableTelemetry.Text = "Lock Down Windows Telemetry";
            btnDisableTelemetry.UseVisualStyleBackColor = false;
            btnDisableTelemetry.Click += btnDisableTelemetry_Click;
            // 
            // tabNetwork
            // 
            tabNetwork.BackColor = Color.White;
            tabNetwork.Controls.Add(txtNetLog);
            tabNetwork.Controls.Add(btnResetWinsock);
            tabNetwork.Controls.Add(btnFlushDNS);
            tabNetwork.Location = new Point(4, 26);
            tabNetwork.Name = "tabNetwork";
            tabNetwork.Padding = new Padding(3);
            tabNetwork.Size = new Size(702, 440);
            tabNetwork.TabIndex = 2;
            tabNetwork.Text = "Network Fixes";
            // 
            // txtNetLog
            // 
            txtNetLog.BackColor = Color.FromArgb(248, 250, 252);
            txtNetLog.BorderStyle = BorderStyle.None;
            txtNetLog.Font = new Font("Consolas", 9.5F);
            txtNetLog.ForeColor = Color.FromArgb(15, 23, 42);
            txtNetLog.Location = new Point(310, 25);
            txtNetLog.Name = "txtNetLog";
            txtNetLog.ReadOnly = true;
            txtNetLog.Size = new Size(370, 395);
            txtNetLog.TabIndex = 2;
            txtNetLog.Text = "";
            // 
            // btnResetWinsock
            // 
            btnResetWinsock.BackColor = Color.FromArgb(240, 249, 255);
            btnResetWinsock.Cursor = Cursors.Hand;
            btnResetWinsock.FlatAppearance.BorderColor = Color.FromArgb(186, 230, 253);
            btnResetWinsock.FlatStyle = FlatStyle.Flat;
            btnResetWinsock.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnResetWinsock.ForeColor = Color.FromArgb(3, 105, 161);
            btnResetWinsock.Location = new Point(20, 85);
            btnResetWinsock.Name = "btnResetWinsock";
            btnResetWinsock.Size = new Size(270, 45);
            btnResetWinsock.TabIndex = 1;
            btnResetWinsock.Text = "Reset Winsock & IP Stack";
            btnResetWinsock.UseVisualStyleBackColor = false;
            btnResetWinsock.Click += btnResetWinsock_Click;
            // 
            // btnFlushDNS
            // 
            btnFlushDNS.BackColor = Color.FromArgb(240, 249, 255);
            btnFlushDNS.Cursor = Cursors.Hand;
            btnFlushDNS.FlatAppearance.BorderColor = Color.FromArgb(186, 230, 253);
            btnFlushDNS.FlatStyle = FlatStyle.Flat;
            btnFlushDNS.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnFlushDNS.ForeColor = Color.FromArgb(3, 105, 161);
            btnFlushDNS.Location = new Point(20, 25);
            btnFlushDNS.Name = "btnFlushDNS";
            btnFlushDNS.Size = new Size(270, 45);
            btnFlushDNS.TabIndex = 0;
            btnFlushDNS.Text = "Flush DNS Cache";
            btnFlushDNS.UseVisualStyleBackColor = false;
            btnFlushDNS.Click += btnFlushDNS_Click;
            // 
            // tabStartup
            // 
            tabStartup.BackColor = Color.White;
            tabStartup.Controls.Add(listStartup);
            tabStartup.Controls.Add(btnDeleteStartup);
            tabStartup.Controls.Add(btnAddStartup);
            tabStartup.Controls.Add(btnRefreshStartup);
            tabStartup.Location = new Point(4, 26);
            tabStartup.Name = "tabStartup";
            tabStartup.Padding = new Padding(3);
            tabStartup.Size = new Size(702, 440);
            tabStartup.TabIndex = 3;
            tabStartup.Text = "Startup Manager";
            // 
            // listStartup
            // 
            listStartup.BackColor = Color.FromArgb(248, 250, 252);
            listStartup.BorderStyle = BorderStyle.None;
            listStartup.Font = new Font("Segoe UI", 9.5F);
            listStartup.ForeColor = Color.FromArgb(15, 23, 42);
            listStartup.FullRowSelect = true;
            listStartup.Location = new Point(20, 80);
            listStartup.Name = "listStartup";
            listStartup.Size = new Size(660, 340);
            listStartup.TabIndex = 3;
            listStartup.UseCompatibleStateImageBehavior = false;
            listStartup.View = View.Details;
            // 
            // btnDeleteStartup
            // 
            btnDeleteStartup.BackColor = Color.FromArgb(239, 68, 68);
            btnDeleteStartup.Cursor = Cursors.Hand;
            btnDeleteStartup.FlatAppearance.BorderSize = 0;
            btnDeleteStartup.FlatStyle = FlatStyle.Flat;
            btnDeleteStartup.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnDeleteStartup.ForeColor = Color.White;
            btnDeleteStartup.Location = new Point(310, 25);
            btnDeleteStartup.Name = "btnDeleteStartup";
            btnDeleteStartup.Size = new Size(140, 40);
            btnDeleteStartup.TabIndex = 2;
            btnDeleteStartup.Text = "Remove Selected";
            btnDeleteStartup.UseVisualStyleBackColor = false;
            btnDeleteStartup.Click += btnDeleteStartup_Click;
            // 
            // btnAddStartup
            // 
            btnAddStartup.BackColor = Color.FromArgb(14, 165, 233);
            btnAddStartup.Cursor = Cursors.Hand;
            btnAddStartup.FlatAppearance.BorderSize = 0;
            btnAddStartup.FlatStyle = FlatStyle.Flat;
            btnAddStartup.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnAddStartup.ForeColor = Color.White;
            btnAddStartup.Location = new Point(165, 25);
            btnAddStartup.Name = "btnAddStartup";
            btnAddStartup.Size = new Size(135, 40);
            btnAddStartup.TabIndex = 1;
            btnAddStartup.Text = "Add Startup App";
            btnAddStartup.UseVisualStyleBackColor = false;
            btnAddStartup.Click += btnAddStartup_Click;
            // 
            // btnRefreshStartup
            // 
            btnRefreshStartup.BackColor = Color.FromArgb(224, 242, 254);
            btnRefreshStartup.Cursor = Cursors.Hand;
            btnRefreshStartup.FlatAppearance.BorderSize = 0;
            btnRefreshStartup.FlatStyle = FlatStyle.Flat;
            btnRefreshStartup.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnRefreshStartup.ForeColor = Color.FromArgb(3, 105, 161);
            btnRefreshStartup.Location = new Point(20, 25);
            btnRefreshStartup.Name = "btnRefreshStartup";
            btnRefreshStartup.Size = new Size(135, 40);
            btnRefreshStartup.TabIndex = 0;
            btnRefreshStartup.Text = "Refresh List";
            btnRefreshStartup.UseVisualStyleBackColor = false;
            btnRefreshStartup.Click += btnRefreshStartup_Click;
            // 
            // pictureBoxAd
            // 
            pictureBoxAd.Cursor = Cursors.Hand;
            pictureBoxAd.Location = new Point(12, 490);
            pictureBoxAd.Name = "pictureBoxAd";
            pictureBoxAd.Size = new Size(710, 75);
            pictureBoxAd.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxAd.TabIndex = 1;
            pictureBoxAd.TabStop = false;
            pictureBoxAd.Visible = false;
            pictureBoxAd.Click += PictureBoxAd_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(734, 576);
            Controls.Add(pictureBoxAd);
            Controls.Add(tabControl);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Advanced PC Cleaner";
            tabControl.ResumeLayout(false);
            tabCleaner.ResumeLayout(false);
            tabCleaner.PerformLayout();
            tabPrivacy.ResumeLayout(false);
            tabNetwork.ResumeLayout(false);
            tabStartup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxAd).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCleaner;
        private System.Windows.Forms.TabPage tabPrivacy;
        private System.Windows.Forms.TabPage tabNetwork;
        private System.Windows.Forms.TabPage tabStartup;

        private System.Windows.Forms.CheckBox chkUserTemp;
        private System.Windows.Forms.CheckBox chkSystemTemp;
        private System.Windows.Forms.CheckBox chkBrowserCache;
        private System.Windows.Forms.CheckBox chkRecycleBin;
        private System.Windows.Forms.Button btnStartClean;
        private System.Windows.Forms.Button btnScanDuplicates;
        private System.Windows.Forms.Button btnShredFile;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.RichTextBox txtLog;

        private System.Windows.Forms.Button btnDisableTelemetry;
        private System.Windows.Forms.Button btnRemoveBloat;
        private System.Windows.Forms.Button btnDisableBing;
        private System.Windows.Forms.RichTextBox txtPrivacyLog;

        private System.Windows.Forms.Button btnFlushDNS;
        private System.Windows.Forms.Button btnResetWinsock;
        private System.Windows.Forms.RichTextBox txtNetLog;

        private System.Windows.Forms.Button btnRefreshStartup;
        private System.Windows.Forms.Button btnAddStartup;
        private System.Windows.Forms.Button btnDeleteStartup;
        private System.Windows.Forms.ListView listStartup;

        private System.Windows.Forms.PictureBox pictureBoxAd;
    }
}