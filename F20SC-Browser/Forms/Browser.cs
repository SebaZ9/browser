using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F20SC_Browser {
    public partial class Browser : Form {

        private TabController tabController;
        private DataController dc;



        public Browser() {
            InitializeComponent();
            KeyPreview = true;

            dc = new DataController();
            dc.ConfirmDatabase();
            tabController = new TabController(tabControl1);

            Shown += (obj, s) => { tabController.AddTab(true); };
        }

        private void Browser_Load(object sender, EventArgs e) {
        }

        #region Button Clicks

        /// <summary>
        /// Tab loads the previous URL from the short history if it exists and changes the icons for the buttons.
        /// </summary>
        private void btnBackArrow_Click(object sender, EventArgs e) {

            if(tabControl1.SelectedTab != null && tabControl1.SelectedTab.Tag != null) {
                if (tabController.GetPreviousPage()) {

                    btnForwardArrow.BackgroundImage = Properties.Resources.forwardArrow_enabled;
                    if (tabController.HasPreviousPage()) {
                        btnBackArrow.BackgroundImage = Properties.Resources.backArrow_enabled;
                    } else {
                        btnBackArrow.BackgroundImage = Properties.Resources.backArrow_disabled;
                    }

                } else {
                    btnBackArrow.BackgroundImage = Properties.Resources.backArrow_disabled;
                }
            }            

        }

        /// <summary>
        /// Tab loads the next URL from the short history if it exists and changes the icons for the buttons.
        /// </summary>
        private void btnForwardArrow_Click(object sender, EventArgs e) {

            if (tabControl1.SelectedTab != null && tabControl1.SelectedTab.Tag != null) {
                if (tabController.GetNextPage()) {

                    btnBackArrow.BackgroundImage = Properties.Resources.backArrow_enabled;
                    if (tabController.HasNextPage()) {
                        btnForwardArrow.BackgroundImage = Properties.Resources.forwardArrow_enabled;
                    } else {
                        btnForwardArrow.BackgroundImage = Properties.Resources.forwardArrow_disabled;
                    }

                } else {
                    btnForwardArrow.BackgroundImage = Properties.Resources.forwardArrow_disabled;
                }
            }

        }

        // Event for opening the Settings Form
        private void OpenSettings(object sender, EventArgs e) {
            tabController.OpenSettings();
            panelSettings.Visible = false;
        }

        // Event for openinig the History Form
        private void OpenHistory(object sender, EventArgs e) {
            tabController.OpenHistory();
            panelSettings.Visible = false;
        }

        // Event for opening the Favourites Form
        private void OpenFavourites(object sender, EventArgs e) {
            tabController.OpenFavourites();
            panelSettings.Visible = false;
        }

        // Event to add a new tab
        private void btnAddTab_Click(object sender, EventArgs e) {
            tabController.AddTab(true);
            
        }

        // Event to toggle the settings panel.
        private void btnSettings_Click(object sender, EventArgs e) {
            if (panelSettings.Visible) {
                panelSettings.Visible = false;
            } else {
                panelSettings.Visible = true;
            }
        }

        // Event to close the window.
        private void btnSettings_Exit_Click(object sender, EventArgs e) {
            Environment.Exit(0);
        }

        // Button for reloading the current tab
        private void btnRefreshPage(object sender, EventArgs e) {
            if (tabControl1.SelectedTab != null && tabControl1.SelectedTab.Tag != null) 
                tabController.ReloadPage();
        }

        // Button for loading the home page to the current tab
        private void btnHomePage(object sender, EventArgs e) {
            if (tabControl1.SelectedTab != null && tabControl1.SelectedTab.Tag != null)
                tabController.AddTabContent(tabController.GetHomePage());
        }

        // Button to toggle if the URL is in the favourites
        private void btnToggleFavourite(object senderm, EventArgs e) {
            if (tabControl1.SelectedTab != null && tabControl1.SelectedTab.Tag != null)
                tabController.ToggleFavourite();
        }

        // Button to close the current tab
        private void btnCloseTab_Click(object sender, EventArgs e) {
            if (tabControl1.SelectedTab != null)
                tabController.CloseTab();
        }


        #endregion

        #region Keyboard Controls

        private void textBox2_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (Uri.IsWellFormedUriString(((TextBox)sender).Text, UriKind.Absolute)) {
                    tabController.AddTabContent(((TextBox)sender).Text);
                    btnForwardArrow.BackgroundImage = Properties.Resources.forwardArrow_disabled;
                    btnBackArrow.BackgroundImage = Properties.Resources.backArrow_enabled;
                } else {
                    MessageBox.Show("URL is not well formed.", "Error", MessageBoxButtons.OK);
                }                
            }
        }

        #endregion


        private void tabController_TabChanged(object sender, EventArgs e) {
            if (((TabControl)sender).SelectedTab != null) {
                textBoxURL.Text = ((TabControl)sender).SelectedTab.Name;
                // Sets correct image for back button
                if (tabController.HasPreviousPage()) {
                    btnBackArrow.BackgroundImage = Properties.Resources.backArrow_enabled;
                } else {
                    btnBackArrow.BackgroundImage = Properties.Resources.backArrow_disabled;
                }

                // Sets correct image for forward button
                if (tabController.HasNextPage()) {
                    btnForwardArrow.BackgroundImage = Properties.Resources.forwardArrow_enabled;
                } else {
                    btnForwardArrow.BackgroundImage = Properties.Resources.forwardArrow_disabled;
                }

                // Sets correct image for favourites button
                if (tabController.FavouriteExists(tabControl1.SelectedTab.Name)) {
                    btnFavourite.BackgroundImage = Properties.Resources.favourite_on;
                } else {
                    btnFavourite.BackgroundImage = Properties.Resources.favourite_off;
                }
            } else {
                // If no tab exists turn all buttons off
                btnBackArrow.BackgroundImage = Properties.Resources.backArrow_disabled;
                btnForwardArrow.BackgroundImage = Properties.Resources.forwardArrow_disabled;
                btnFavourite.BackgroundImage = Properties.Resources.favourite_off;
            }
        }
        private void tabController_TabAdded(object sender, ControlEventArgs e) {
            if(((TabControl)sender).SelectedTab != null) textBoxURL.Text = ((TabControl)sender).SelectedTab.Name;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            tabController.ProcessKeyBinds(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
