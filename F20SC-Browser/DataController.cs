using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F20SC_Browser {

    public enum DBTables { History, Favourites }

    public class DataController {

        string dbLocation;
        string dbFolder;

        Dictionary<DBTables, string> dbRows = new Dictionary<DBTables, string>() {
            { DBTables.History, "(url, date)" },
            { DBTables.Favourites, "(url, givenname)" }
        };

        public DataController() {
            dbFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "DefinetlyNotChrome"
            );
            dbLocation = Path.Combine(
                dbFolder,
                "sqlitedb.db"
            );            
        }
       

        /// <summary>
        /// Executes a "NonQuery" on the databse.
        /// </summary>
        /// <param name="query">The string query</param>
        private void ExecuteNonQuery(string query) {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbLocation};Version=3")) {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection)) {

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }

                }
            
            }
        }

        /// <summary>
        /// Confirms if the database and the correct tables exists, if not then new tables are created.
        /// </summary>
        public void ConfirmDatabase() {

            // Create db file if it doesnt exist
            Directory.CreateDirectory(dbFolder);
            if (!File.Exists(dbLocation)) {
                SQLiteConnection.CreateFile(dbLocation);
            }

            // Create history table if it doesnt exits
            string historyCheck = "CREATE TABLE IF NOT EXISTS History (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, url text VARCHAR(256) NOT NULL, date text VARCHAR(256) NOT NULL);";
            // Create favourites table if it doesnt exits
            string favouritesCheck = "CREATE TABLE IF NOT EXISTS Favourites (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, url text VARCHAR(256) NOT NULL, givenname text VARCHAR(256) NOT NULL);";

            ExecuteNonQuery(historyCheck);
            ExecuteNonQuery(favouritesCheck);
        }

        /// <summary>
        /// Adds an entry to a table in the current DB.
        /// </summary>
        /// <param name="entry">Array of the data to be inserted in the order of the columns in the table</param>
        /// <param name="table">DBTables enum contains all the available tables.</param>
        public void AddToDB(string[] entry, DBTables table) {

            string values = "";
            foreach (string s in entry) {
                values += $"'{s}',";
            }

            string query = $"INSERT INTO {table} {dbRows[table]} VALUES ({values.Remove(values.Length - 1)})";

            ExecuteNonQuery(query);

        }

        /// <summary>
        /// Removes an item from a database table with the given id.
        /// </summary>
        /// <param name="entry">The string of the entry in the database.</param>
        /// <param name="column">The name of the column of the entry.</param>
        /// <param name="table">DBTables enum contains all the available tables.</param>
        public void RemoveFromDB(string entry, string column, DBTables table) {
            if(entry.Equals("*")) {
                ExecuteNonQuery($"DELETE FROM {table};");
            } else {
                ExecuteNonQuery($"DELETE FROM {table} WHERE {column}='{entry}';");
            }
        }

        /// <summary>
        /// Updates an entry in the databse.
        /// </summary>
        /// <param name="table">The table of the updated entry</param>
        /// <param name="column">The column that is being updated</param>
        /// <param name="newVal">The new value of the column</param>
        /// <param name="conditionColumn">Left hand side of the condition for the SQL query</param>
        /// <param name="condition">Right hand side of the condition for the SQL query</param>
        public void UpdateDB(DBTables table, string column, string newVal, string conditionColumn, string condition) {
            string query = $"UPDATE {table} SET {column}='{newVal}' WHERE {conditionColumn}='{condition}';";
            ExecuteNonQuery(query);
        }

        /// <summary>
        /// Get all the rows from a table.
        /// </summary>
        /// <param name="table">DBTables enum contains all the available tables.</param>
        public List<string> GetRows(DBTables table) {

            List<string> list = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection()) {

                connection.ConnectionString = $"Data Source={dbLocation};Version=3";
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand()) {

                    command.Connection = connection;
                    command.CommandText = $"SELECT * FROM {table}";
                    try {

                        SQLiteDataReader rdr = command.ExecuteReader();

                        while (rdr.Read()) {
                            for(int i =0; i < rdr.FieldCount; i++) {
                                list.Add(Convert.ToString(rdr[i]));
                            }
                        }
                    } catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }
                connection.Close();
            }

            return list;
        }

        public DataTable GetRowsData(DBTables table) {

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbLocation};Version=3")) {

                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection)) {

                    command.CommandText = $"SELECT * FROM {table};";
                    try {

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command)) {

                            DataTable dTable = new DataTable();
                            adapter.Fill(dTable);
                            return dTable;
                        }
                        
                    } catch (Exception e) {
                        Console.WriteLine(e.Message);
                        return null;
                    }
                }
            }

        }


    }

}
