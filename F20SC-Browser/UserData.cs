using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F20SC_Browser {

    /// <summary>
    /// User Data class is used to store the users data such as the home page, key binds and favourites.
    /// </summary>
    class UserData {

        // Static object for access in other forms.
        public static UserData userData;
        string HomePage;

        // Key binds
        public delegate void KeyAction();
        KeyBind closeTab;
        KeyBind openSettings;
        KeyBind openHistory;
        KeyBind openFavourites;
        KeyBind newTab;
        List<KeyBind> keyBinds;

        // Favourites
        Dictionary<string, string> favouritesNames;
        DataController data;


        public UserData(TabController tc) {
            userData = this;
            // Load in home page
            HomePage = (string)Properties.Settings.Default["homePage"];
            if(!Uri.IsWellFormedUriString(HomePage, UriKind.Absolute)) {
                HomePage = "";
                Properties.Settings.Default["homePage"] = "";
                Properties.Settings.Default.Save();
            }
            // Create Key binds for actions.
            newTab = new KeyBind(new KeyAction(tc.CreateTab), Keys.N, Keys.Control);
            closeTab = new KeyBind(new KeyAction(tc.CloseTab), Keys.W, Keys.Control);
            openSettings = new KeyBind(new KeyAction(tc.OpenSettings), Keys.S, Keys.Control);
            openHistory = new KeyBind(new KeyAction(tc.OpenHistory), Keys.H, Keys.Control);
            openFavourites = new KeyBind(new KeyAction(tc.OpenFavourites), Keys.F, Keys.Control);

            keyBinds = new List<KeyBind>() {
                closeTab,
                openFavourites,
                openHistory,
                openSettings,
                newTab
            };

            // Load in favourite URLs from database
            favouritesNames = new Dictionary<string, string>();
            data = new DataController();
            PopulateSet();
        }

        /// <summary>
        /// Checks the saved keybinds to check if they need executed.
        /// </summary>
        /// <param name="e">Keys is passed in from Key Down event in main form.</param>
        public void CheckKeyBinds(Keys e) {
            foreach(KeyBind b in keyBinds) {
                if(e == (b.controlVal1 | b.controlVal2 | b.controlVal3 | b.keys)) {
                    b.action.DynamicInvoke();
                }
            }
        }

        /// <summary>
        /// Gets the users home page.
        /// </summary>
        /// <returns>Returns a string with the Home Page URL</returns>
        public string GetHomePage() {
            return HomePage;
        }

        /// <summary>
        /// Sets a new home page for the user and saves it in the properties.
        /// </summary>
        /// <param name="url">The home page url.</param>
        public void SetHomePage(string url) {
            if(!Uri.IsWellFormedUriString(url, UriKind.Absolute)) {
                MessageBox.Show("URL is not well formed.", "Error", MessageBoxButtons.OK);
                return;
            }
            HomePage = url;
            Properties.Settings.Default["homePage"] = url;
            Properties.Settings.Default.Save();

        }

        /// <summary>
        /// Loads in the data from the favourites databse and inserts them into a Dictionary.
        /// </summary>
        private void PopulateSet() {
            List<string> favList = data.GetRows(DBTables.Favourites);

            for (int i = 0; i < favList.Count; i += 3) {
                favouritesNames.Add(favList[i + 1], favList[i + 2]);
            }
        }

        /// <summary>
        /// Checks if the given url is currently in the favourite list.
        /// </summary>
        /// <param name="fav">The URL to be checked.</param>
        public bool FavouriteExitst(string fav) {
            return favouritesNames.Keys.Contains(fav);
        }

        /// <summary>
        /// Adds a given URL to the favourite list and database.
        /// </summary>
        /// <param name="fav">The URL to be added</param>
        public void AddToFavourites(string fav, string givenname = "") {
            if (!FavouriteExitst(fav)) {
                data.AddToDB(new string[] { fav, givenname }, DBTables.Favourites);
                favouritesNames.Add(fav, givenname);
            }
        }

        /// <summary>
        /// Removes a given URL from the favourites list and database.
        /// </summary>
        /// <param name="fav">The URL to be removed</param>
        public void RemoveFromFavourites(string fav) {
            if (FavouriteExitst(fav)) {
                data.RemoveFromDB(fav, "url", DBTables.Favourites);
                favouritesNames.Remove(fav);
            }
        }

        /// <summary>
        /// Changes the url and or givenname for a favourite in the Dictionary and the databse
        /// </summary>
        /// <param name="toChange">The URL to change.</param>
        /// <param name="newUrl">The new URL.</param>
        /// <param name="newGivenName">The new Given Name.</param>
        public void ModifyFavourite(string toChange, string newUrl, string newGivenName) {
            if (FavouriteExitst(toChange)) {
                favouritesNames.Remove(toChange);
                favouritesNames.Add(newUrl, newGivenName);
                data.UpdateDB(DBTables.Favourites, "url", newUrl, "url", toChange);
                data.UpdateDB(DBTables.Favourites, "givenname", newGivenName, "url", newUrl);
            }
        }

        /// <summary>
        /// Returns the hashset of the current favourites.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> GetFavourites() {
            return favouritesNames;
        }


    }

    /// <summary>
    /// Key bind struct is for creating custom key binds, 
    /// Can assign an action, a Key and up to three control keys. Alt | Shift | Ctrl
    /// </summary>
    struct KeyBind {
        public Keys controlVal1 { get; private set; }
        public Keys controlVal2 { get; private set; }
        public Keys controlVal3 { get; private set; }
        public Keys keys { get; private set; }
        public Delegate action { get; private set; }

        public KeyBind(Delegate action, Keys keys, Keys controlVal1, Keys controlVal2 = 0, Keys controlVal3 = 0) {
            this.keys = keys; 
            this.controlVal1 = controlVal1;
            this.controlVal2 = controlVal2;
            this.controlVal3 = controlVal3;
            this.action = action;        
        }

    }

}
