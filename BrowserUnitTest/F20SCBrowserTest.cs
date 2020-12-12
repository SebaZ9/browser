using Microsoft.VisualStudio.TestTools.UnitTesting;
using F20SC_Browser;
using System.Collections.Generic;
using System;
using System.Net;

namespace BrowserUnitTest {
    [TestClass]
    public class F20SCBrowserTest {

        [TestMethod]
        public void WebRequestsTest() {

            WebRequests wr = new WebRequests();

            // Testing valid URL input 
            
            F20SC_Browser.WebResponse test200 = wr.GetURL("http://httpstat.us/200");
            F20SC_Browser.WebResponse test202 = wr.GetURL("http://httpstat.us/202");
            F20SC_Browser.WebResponse test204 = wr.GetURL("http://httpstat.us/204");
            F20SC_Browser.WebResponse test400 = wr.GetURL("http://httpstat.us/400");
            F20SC_Browser.WebResponse test403 = wr.GetURL("http://httpstat.us/403");
            F20SC_Browser.WebResponse test404 = wr.GetURL("http://httpstat.us/404");
            F20SC_Browser.WebResponse test500 = wr.GetURL("http://httpstat.us/500");
            F20SC_Browser.WebResponse test501 = wr.GetURL("http://httpstat.us/501");

            // Testing wrong URL input
            F20SC_Browser.WebResponse invalid1 = wr.GetURL("httptpstat.us/200");
            F20SC_Browser.WebResponse invalid2 = wr.GetURL("");
            F20SC_Browser.WebResponse invalid3 = wr.GetURL("http//1/");

            F20SC_Browser.WebResponse google200 = wr.GetURL("http://google.com");
            F20SC_Browser.WebResponse google404 = wr.GetURL("http://gooogle.com/123");

            Assert.AreEqual(200, test200.statusCode);
            Assert.AreEqual(202, test202.statusCode);
            Assert.AreEqual(204, test204.statusCode);
            Assert.AreEqual(400, test400.statusCode);
            Assert.AreEqual(403, test403.statusCode);
            Assert.AreEqual(404, test404.statusCode);
            Assert.AreEqual(500, test500.statusCode);
            Assert.AreEqual(501, test501.statusCode);

            Assert.AreEqual(-1, invalid1.statusCode);
            Assert.AreEqual(-1, invalid2.statusCode);
            Assert.AreEqual(-1, invalid3.statusCode);

            Assert.AreEqual(200, google200.statusCode);
            Assert.AreEqual(404, google404.statusCode);
        }

        [TestMethod]
        public void ShortHistoryTestString() {

            ShortHistory<string> shTest1 = new ShortHistory<string>();

            // NewPageLoaded is the function to add a new value to the head node.
            shTest1.NewPageLoaded("url1");
            shTest1.NewPageLoaded("url2");
            shTest1.NewPageLoaded("url3");
            shTest1.NewPageLoaded("url4");
            // Adding 4 url's, url4 is the newest one.

            Assert.AreEqual("url4", shTest1.GetCurrentValue()); // Current loaded page is url4
            Assert.AreEqual("url3", shTest1.TraverseBack());    // Traverse back takes us to url3
            Assert.AreEqual("url2", shTest1.TraverseBack());    // Traverse back takes us to url2
            Assert.AreEqual("url2", shTest1.GetCurrentValue());    // Current loaded page is still url2
            Assert.AreEqual("url1", shTest1.TraverseBack());    // Traverse back takes us to url1
            Assert.AreEqual(null, shTest1.TraverseBack());    // Traverse back, since its last page in list it returns null
            Assert.AreEqual(null, shTest1.TraverseBack());    // Checking traverse back again.
            Assert.AreEqual("url2", shTest1.TraverseForward());    // Traverse forward takes us back to url2
            Assert.AreEqual("url3", shTest1.TraverseForward());    // Traverse forward takes us back to url3
            Assert.AreEqual("url4", shTest1.TraverseForward());    // Traverse forward takes us back to start, url4
            Assert.AreEqual("url4", shTest1.GetCurrentValue());    // Current loaded page is url4
            Assert.AreEqual(null, shTest1.TraverseForward());    // Traverse forward, since this is newest page it should return null
            Assert.AreEqual(null, shTest1.TraverseForward());    // Testing traverse forward for another null.
        }

        /// <summary>
        /// The database test use the same tables the browser uses,
        /// so in order for the tests to pass the tables must be empty.
        /// Each time the test is run the databse is cleared.
        /// </summary>
        [TestMethod]
        public void DataBaseControllerTest() {

            DataController dc = new DataController();

            dc.RemoveFromDB("*", "url", DBTables.Favourites);
            Console.WriteLine("GEts here");
            dc.RemoveFromDB("*", "url", DBTables.History);


            // Both databses should be empyy
            Assert.AreEqual(0, dc.GetRows(DBTables.History).Count);
            Assert.AreEqual(0, dc.GetRows(DBTables.Favourites).Count);

            dc.AddToDB(new string[] {"url1", DateTime.Now.ToString()}, DBTables.History);   // Add url1 to DB.
            dc.AddToDB(new string[] {"url2", DateTime.Now.ToString()}, DBTables.History);   // Add url2 to DB.
            dc.AddToDB(new string[] {"url3", DateTime.Now.ToString()}, DBTables.History);   // Add url3 to DB.
            dc.AddToDB(new string[] {"url3", DateTime.Now.ToString()}, DBTables.History);   // Add url3 again to DB.

            // Check if the databse has 4 entries.
            // The 4 entries are * 3 because the list has values for, id url and date for each entry
            Assert.AreEqual(4*3, dc.GetRows(DBTables.History).Count);

            // Add another 2 entries
            dc.AddToDB(new string[] { "url4", DateTime.Now.ToString() }, DBTables.History); 
            dc.AddToDB(new string[] { "url5", DateTime.Now.ToString() }, DBTables.History); 

            Assert.AreEqual(6*3, dc.GetRows(DBTables.History).Count);

            // Remove some entires
            dc.RemoveFromDB("url1", "url", DBTables.History);
            dc.RemoveFromDB("url2", "url", DBTables.History);

            // Count should now be back at 4 after 2 deletes.
            Assert.AreEqual(4*3, dc.GetRows(DBTables.History).Count);

            // The rest of the URLs in history should still be in DV
            List<string> row = dc.GetRows(DBTables.History);

            // The rows contain - Id, url, date. so index = Row Number * 3, + 0 for id, + 1 for url and + 2 for date
            Assert.AreEqual("url3", row[1]);
            Assert.AreEqual("url3", row[4]);
            Assert.AreEqual("url4", row[7]);
            Assert.AreEqual("url5", row[10]);

        }


    }
}
