namespace F20SC_Browser {
    partial class Browser {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Browser));
            this.panelURL = new System.Windows.Forms.Panel();
            this.btnCloseTab = new System.Windows.Forms.Panel();
            this.btnNewTab = new System.Windows.Forms.Panel();
            this.btnFavourite = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Panel();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Panel();
            this.panelDivider = new System.Windows.Forms.Panel();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.labelSettings_Exit = new System.Windows.Forms.Label();
            this.labelSettings_Favourites = new System.Windows.Forms.Label();
            this.labelSettings_History = new System.Windows.Forms.Label();
            this.labelSettings_Settings = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnForwardArrow = new System.Windows.Forms.Panel();
            this.btnBackArrow = new System.Windows.Forms.Panel();
            this.panelURL.SuspendLayout();
            this.panelSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelURL
            // 
            this.panelURL.BackColor = System.Drawing.Color.White;
            this.panelURL.Controls.Add(this.btnCloseTab);
            this.panelURL.Controls.Add(this.btnNewTab);
            this.panelURL.Controls.Add(this.btnFavourite);
            this.panelURL.Controls.Add(this.btnHome);
            this.panelURL.Controls.Add(this.btnSettings);
            this.panelURL.Controls.Add(this.textBoxURL);
            this.panelURL.Controls.Add(this.btnRefresh);
            this.panelURL.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelURL.Location = new System.Drawing.Point(0, 0);
            this.panelURL.Margin = new System.Windows.Forms.Padding(0);
            this.panelURL.Name = "panelURL";
            this.panelURL.Size = new System.Drawing.Size(951, 36);
            this.panelURL.TabIndex = 0;
            // 
            // btnCloseTab
            // 
            this.btnCloseTab.BackColor = System.Drawing.Color.White;
            this.btnCloseTab.BackgroundImage = global::F20SC_Browser.Properties.Resources.exit;
            this.btnCloseTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCloseTab.Location = new System.Drawing.Point(144, 2);
            this.btnCloseTab.Margin = new System.Windows.Forms.Padding(0);
            this.btnCloseTab.Name = "btnCloseTab";
            this.btnCloseTab.Size = new System.Drawing.Size(32, 32);
            this.btnCloseTab.TabIndex = 2;
            this.btnCloseTab.Click += new System.EventHandler(this.btnCloseTab_Click);
            // 
            // btnNewTab
            // 
            this.btnNewTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewTab.BackColor = System.Drawing.Color.White;
            this.btnNewTab.BackgroundImage = global::F20SC_Browser.Properties.Resources.add;
            this.btnNewTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNewTab.Location = new System.Drawing.Point(839, 2);
            this.btnNewTab.Margin = new System.Windows.Forms.Padding(0);
            this.btnNewTab.Name = "btnNewTab";
            this.btnNewTab.Size = new System.Drawing.Size(32, 32);
            this.btnNewTab.TabIndex = 7;
            this.btnNewTab.Click += new System.EventHandler(this.btnAddTab_Click);
            // 
            // btnFavourite
            // 
            this.btnFavourite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFavourite.BackColor = System.Drawing.Color.White;
            this.btnFavourite.BackgroundImage = global::F20SC_Browser.Properties.Resources.favourite_off;
            this.btnFavourite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFavourite.Location = new System.Drawing.Point(871, 2);
            this.btnFavourite.Margin = new System.Windows.Forms.Padding(0);
            this.btnFavourite.Name = "btnFavourite";
            this.btnFavourite.Size = new System.Drawing.Size(32, 32);
            this.btnFavourite.TabIndex = 6;
            this.btnFavourite.Click += new System.EventHandler(this.btnToggleFavourite);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.White;
            this.btnHome.BackgroundImage = global::F20SC_Browser.Properties.Resources.home;
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnHome.Location = new System.Drawing.Point(112, 2);
            this.btnHome.Margin = new System.Windows.Forms.Padding(0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(32, 32);
            this.btnHome.TabIndex = 1;
            this.btnHome.Click += new System.EventHandler(this.btnHomePage);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.BackColor = System.Drawing.Color.White;
            this.btnSettings.BackgroundImage = global::F20SC_Browser.Properties.Resources.settings;
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSettings.Location = new System.Drawing.Point(903, 2);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(0);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(32, 32);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // textBoxURL
            // 
            this.textBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxURL.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.textBoxURL.Location = new System.Drawing.Point(176, 6);
            this.textBoxURL.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(663, 24);
            this.textBoxURL.TabIndex = 0;
            this.textBoxURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            this.textBoxURL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyUp);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.BackgroundImage = global::F20SC_Browser.Properties.Resources.refresh;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefresh.Location = new System.Drawing.Point(80, 2);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 32);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefreshPage);
            // 
            // panelDivider
            // 
            this.panelDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(220)))), ((int)(((byte)(224)))));
            this.panelDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDivider.Location = new System.Drawing.Point(0, 36);
            this.panelDivider.Name = "panelDivider";
            this.panelDivider.Size = new System.Drawing.Size(951, 1);
            this.panelDivider.TabIndex = 1;
            // 
            // panelSettings
            // 
            this.panelSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSettings.BackColor = System.Drawing.Color.White;
            this.panelSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSettings.Controls.Add(this.labelSettings_Exit);
            this.panelSettings.Controls.Add(this.labelSettings_Favourites);
            this.panelSettings.Controls.Add(this.labelSettings_History);
            this.panelSettings.Controls.Add(this.labelSettings_Settings);
            this.panelSettings.Location = new System.Drawing.Point(771, 36);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(180, 131);
            this.panelSettings.TabIndex = 0;
            this.panelSettings.Visible = false;
            // 
            // labelSettings_Exit
            // 
            this.labelSettings_Exit.AutoSize = true;
            this.labelSettings_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettings_Exit.Location = new System.Drawing.Point(4, 99);
            this.labelSettings_Exit.Name = "labelSettings_Exit";
            this.labelSettings_Exit.Size = new System.Drawing.Size(35, 20);
            this.labelSettings_Exit.TabIndex = 3;
            this.labelSettings_Exit.Text = "Exit";
            this.labelSettings_Exit.Click += new System.EventHandler(this.btnSettings_Exit_Click);
            // 
            // labelSettings_Favourites
            // 
            this.labelSettings_Favourites.AutoSize = true;
            this.labelSettings_Favourites.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettings_Favourites.Location = new System.Drawing.Point(4, 67);
            this.labelSettings_Favourites.Name = "labelSettings_Favourites";
            this.labelSettings_Favourites.Size = new System.Drawing.Size(83, 20);
            this.labelSettings_Favourites.TabIndex = 2;
            this.labelSettings_Favourites.Text = "Favourites";
            this.labelSettings_Favourites.Click += new System.EventHandler(this.OpenFavourites);
            // 
            // labelSettings_History
            // 
            this.labelSettings_History.AutoSize = true;
            this.labelSettings_History.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettings_History.Location = new System.Drawing.Point(4, 36);
            this.labelSettings_History.Name = "labelSettings_History";
            this.labelSettings_History.Size = new System.Drawing.Size(58, 20);
            this.labelSettings_History.TabIndex = 1;
            this.labelSettings_History.Text = "History";
            this.labelSettings_History.Click += new System.EventHandler(this.OpenHistory);
            // 
            // labelSettings_Settings
            // 
            this.labelSettings_Settings.AutoSize = true;
            this.labelSettings_Settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettings_Settings.Location = new System.Drawing.Point(4, 7);
            this.labelSettings_Settings.Name = "labelSettings_Settings";
            this.labelSettings_Settings.Size = new System.Drawing.Size(68, 20);
            this.labelSettings_Settings.TabIndex = 0;
            this.labelSettings_Settings.Text = "Settings";
            this.labelSettings_Settings.Click += new System.EventHandler(this.OpenSettings);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(0, 36);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(951, 578);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.TabStop = false;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabController_TabChanged);
            this.tabControl1.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tabController_TabAdded);
            // 
            // btnForwardArrow
            // 
            this.btnForwardArrow.BackColor = System.Drawing.Color.White;
            this.btnForwardArrow.BackgroundImage = global::F20SC_Browser.Properties.Resources.forwardArrow_disabled;
            this.btnForwardArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnForwardArrow.Location = new System.Drawing.Point(48, 2);
            this.btnForwardArrow.Margin = new System.Windows.Forms.Padding(0);
            this.btnForwardArrow.Name = "btnForwardArrow";
            this.btnForwardArrow.Size = new System.Drawing.Size(32, 32);
            this.btnForwardArrow.TabIndex = 3;
            this.btnForwardArrow.Click += new System.EventHandler(this.btnForwardArrow_Click);
            // 
            // btnBackArrow
            // 
            this.btnBackArrow.BackColor = System.Drawing.Color.White;
            this.btnBackArrow.BackgroundImage = global::F20SC_Browser.Properties.Resources.backArrow_disabled;
            this.btnBackArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBackArrow.Location = new System.Drawing.Point(16, 2);
            this.btnBackArrow.Margin = new System.Windows.Forms.Padding(0);
            this.btnBackArrow.Name = "btnBackArrow";
            this.btnBackArrow.Size = new System.Drawing.Size(32, 32);
            this.btnBackArrow.TabIndex = 4;
            this.btnBackArrow.Click += new System.EventHandler(this.btnBackArrow_Click);
            // 
            // Browser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 613);
            this.Controls.Add(this.panelSettings);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnForwardArrow);
            this.Controls.Add(this.btnBackArrow);
            this.Controls.Add(this.panelDivider);
            this.Controls.Add(this.panelURL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Browser";
            this.Text = "Definitely Not Chrome";
            this.Load += new System.EventHandler(this.Browser_Load);
            this.panelURL.ResumeLayout(false);
            this.panelURL.PerformLayout();
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelURL;
        private System.Windows.Forms.Panel panelDivider;
        private System.Windows.Forms.Panel btnBackArrow;
        private System.Windows.Forms.Panel btnForwardArrow;
        private System.Windows.Forms.Panel btnRefresh;
        private System.Windows.Forms.Panel btnSettings;
        private System.Windows.Forms.Panel btnHome;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Label labelSettings_Settings;
        private System.Windows.Forms.Label labelSettings_Exit;
        private System.Windows.Forms.Label labelSettings_Favourites;
        private System.Windows.Forms.Label labelSettings_History;
        private System.Windows.Forms.Panel btnFavourite;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel btnNewTab;
        private System.Windows.Forms.Panel btnCloseTab;
    }
}