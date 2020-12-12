using F20SC_Browser.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F20SC_Browser {

    /// <summary>
    /// Main class for the tab control. Used for all the interactions with the tabs.
    /// </summary>
    class TabController {

        public static TabController tc;

        TabControl tabControl;
        WebRequests webRequests;
        DataController dataController;
        Dictionary<Guid, ShortHistory<string>> tabs;
        UserData userData;

        public TabController(TabControl tabControl) {
            this.tabControl = tabControl;
            tabs = new Dictionary<Guid, ShortHistory<string>>();
            webRequests = new WebRequests();
            dataController = new DataController();
            tc = this;
            userData = new UserData(this);
        }

        /// <summary>
        /// Creates a new tab for the tab controller.
        /// </summary>
        /// <param name="homePage">Bool that decides if the Home Page should be automatically loaded.</param>
        public void AddTab(bool homePage=false) {
            TabPage tabPage = new TabPage();
            tabPage.Text = "New Tab";
            tabPage.Tag = Guid.NewGuid();
            tabControl.Controls.Add(tabPage);

            ShortHistory<string> sHistory = new ShortHistory<string>();

            tabs.Add((Guid)tabPage.Tag, sHistory);

            // Create and setup the html body
            TextBox textBox = new TextBox();
            textBox.Text = "No home page set." + Environment.NewLine + "Visit settings to set home page.";
            textBox.BorderStyle = BorderStyle.None;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.ReadOnly = true;
            textBox.WordWrap = true;
            tabPage.Controls.Add(textBox);


            // Focus the new created tab
            tabControl.SelectTab(tabControl.TabPages.Count - 1);
            if (homePage && userData.GetHomePage() != "") AddTabContent(userData.GetHomePage());
        }

        /// <summary>
        /// A method for calling the create tab with no paramters. Used for the key bind delegate.
        /// </summary>
        public void CreateTab() { AddTab(true); }

        /// <summary>
        /// Loads the url dat into the currently selected tab.
        /// </summary>
        /// <params name="url"> The url link of the page to be loaded. </params>
        /// <params name="saveHistory"> Boolean to set if the url should be saved in the short history. </params>
        public void AddTabContent(string url, bool saveHistory = true) {
            // Check if a tab is created. If not create one.
            if (tabControl.SelectedTab == null) AddTab();
            if (tabControl.SelectedTab.Tag == null) AddTab();
            // Get the URL response
            WebResponse response = webRequests.GetURL(url);
            // Select the text box 
            TextBox body = tabControl.SelectedTab.Controls.OfType<TextBox>().ToList()[0];
            // Set the tab title to the url
            tabControl.SelectedTab.Text = Regex.Match(response.responseBody, @"<title[^>]*>(.*?)</title>", RegexOptions.IgnoreCase).Groups[1].Value;
            tabControl.SelectedTab.Name = response.url;
            // Save the requested url to the history
            if (saveHistory) { 
                tabs[(Guid)tabControl.SelectedTab.Tag].NewPageLoaded(url);
                dataController.AddToDB(new string[] { url, DateTime.Now.ToString() }, DBTables.History);
            }
            // Add the response to the txt body
            if(response.statusCode != -1) {
                body.Text = "Status Code: " + response.statusCode.ToString() + " - " + (HttpStatusCode)response.statusCode + Environment.NewLine + Environment.NewLine;
                body.Text += "Response Headers: " + Environment.NewLine + response.responseHeaders + Environment.NewLine;
                body.Text += "Main Body: " + Environment.NewLine + response.responseBody;
            } else {
                body.Text = $"The URL submitted is not valid: {url} {Environment.NewLine} Please nter a new URL.";
            }
            
            // Check if the URL is favourited
            if (userData.FavouriteExitst(url)) {
                Form.ActiveForm.Controls.Find("btnFavourite",true)[0].BackgroundImage = Properties.Resources.favourite_on;
            } else {
                Form.ActiveForm.Controls.Find("btnFavourite",true)[0].BackgroundImage = Properties.Resources.favourite_off;
            }

            Form.ActiveForm.Controls.Find("textBoxURL", true)[0].Text = url;
        }

        /// <summary>
        /// Creates a new form that contains the settings page and inserts it into a tab
        /// </summary>
        public void OpenSettings() {
            TabPage tabPage = new TabPage();
            tabPage.Text = "Settings";
            tabControl.Controls.Add(tabPage);

            Settings settings = new Settings() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            tabPage.Controls.Add(settings);
            settings.Show();

            // Focus the new created tab
            tabControl.SelectTab(tabControl.TabPages.Count - 1);
        }

        /// <summary>
        /// Creates a new form that contains the favourites page and inserts it into a tab
        /// </summary>
        public void OpenFavourites() {
            TabPage tabPage = new TabPage();
            tabPage.Text = "Favourites";
            tabControl.Controls.Add(tabPage);

            Favourites favourites = new Favourites() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            tabPage.Controls.Add(favourites);
            favourites.Show();

            // Focus the new created tab
            tabControl.SelectTab(tabControl.TabPages.Count - 1);
        }

        /// <summary>
        /// Creates a new form that contains the History page and inserts it into a tab
        /// </summary>
        public void OpenHistory() {
            TabPage tabPage = new TabPage();
            tabPage.Text = "History";
            tabControl.Controls.Add(tabPage);

            History history = new History() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            tabPage.Controls.Add(history);
            history.Show();

            // Focus the new created tab
            tabControl.SelectTab(tabControl.TabPages.Count - 1);
        }

        /// <summary>
        /// Gets the previous page for the tabs history if there is one.
        /// </summary>
        /// <returns> Return a true or false depending if the new page was set </returns>
        public bool GetPreviousPage() {
            ShortHistory<string> sh = tabs[(Guid)tabControl.SelectedTab.Tag];
            if (sh.HasPrevPage()) {
                string res = sh.TraverseBack();
                AddTabContent(res, false);
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Checks if the current Short history has a previous page.
        /// </summary>
        /// <returns>Bool depending if the previous node is null.</returns>
        public bool HasPreviousPage() {
            if(tabControl.SelectedTab.Tag != null) {
                return tabs[(Guid)tabControl.SelectedTab.Tag].HasPrevPage();
            } else {
                return false;
            }
        }

        /// <summary>
        /// Gets the next page for the tabs history if there is one.
        /// </summary>
        /// <returns> Return a true or false depending if the new page was set </returns>
        public bool GetNextPage() {
            ShortHistory<string> sh = tabs[(Guid)tabControl.SelectedTab.Tag];
            if (sh.HasNextPage()) {
                string res = sh.TraverseForward();
                AddTabContent(res, false);
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Checks if the current Short history has a next page.
        /// </summary>
        /// <returns>Bool depending if the next node is null.</returns>
        public bool HasNextPage() {
            if (tabControl.SelectedTab.Tag != null) {
                return tabs[(Guid)tabControl.SelectedTab.Tag].HasNextPage();
            } else {
                return false;
            }
        }

        /// <summary>
        /// Method for reloading the current tab with the same page.
        /// </summary>
        public void ReloadPage() {
            AddTabContent(tabs[(Guid)tabControl.SelectedTab.Tag].GetCurrentValue(), false);
        }


        /// <summary>
        /// Adds or Removes the current URL to the databse depending on its current value. 
        /// </summary>
        public void ToggleFavourite() {
            string url = tabControl.SelectedTab.Name;
            if (userData.FavouriteExitst(url)) {
                userData.RemoveFromFavourites(url);
                Form.ActiveForm.Controls.Find("btnFavourite", true)[0].BackgroundImage = Properties.Resources.favourite_off;
            } else {
                userData.AddToFavourites(url);
                Form.ActiveForm.Controls.Find("btnFavourite", true)[0].BackgroundImage = Properties.Resources.favourite_on;
            }
        }

        /// <summary>
        /// Method to check if the current website exists in the favourites.
        /// </summary>
        /// <param name="url">The URL to be checked</param>
        /// <returns></returns>
        public bool FavouriteExists(string url) {
            return userData.FavouriteExitst(url);
        }

        /// <summary>
        /// Closes the currently opened tab and removes its short history.
        /// </summary>
        public void CloseTab() {
            TabPage curr = tabControl.SelectedTab;
            if (curr.Tag != null) {
                tabs.Remove((Guid)curr.Tag);
            }
            tabControl.TabPages.Remove(curr);
        }

        /// <summary>
        /// Returns the Home page which is stored in the user data.
        /// </summary>
        public string GetHomePage() {
            return userData.GetHomePage();
        }

        /// <summary>
        /// Processes the user set keybinds.
        /// </summary>
        /// <param name="keyData">Key parameter from "ProcessCmdKey"</param>
        public void ProcessKeyBinds(Keys keyData) {
            userData.CheckKeyBinds(keyData);
        }

    }

}
