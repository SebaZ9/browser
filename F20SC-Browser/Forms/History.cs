using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Windows.Controls;

namespace F20SC_Browser.Forms {
    public partial class History : Form {

        DataController dataController;
        TabController tc;
        Dictionary<Guid, int> dict;

        int rowCounter = 2;

        public History() {
            InitializeComponent();

            dataController = new DataController();
            dict = new Dictionary<Guid, int>();
            List<string> res = dataController.GetRows(DBTables.History);
            for (int i = 0; i < res.Count; i += 3) {
                AddHistoryLabel(res[i+2], res[i+1]);
            }

            labelDeleteAll.Click += (obj, e) => {
                dataController.RemoveFromDB("*", "url", DBTables.History);
                for(int i = 2; i < tableLayoutPanel1.RowCount -1; i++) {
                    tableLayoutPanel1.Controls.Remove(
                        tableLayoutPanel1.GetControlFromPosition(0, i));
                }
            };
            tc = TabController.tc;
        }



        /// <summary>
        /// Adds a new entry to the history page.
        /// </summary>
        /// <param name="string"> The string that will be used to show the URL of the page. </param>
        private void AddHistoryLabel(string date, string url) {
            // Creates a new row to contain the new panel.
            RowStyle newRow = new RowStyle(SizeType.Absolute, 50);
            tableLayoutPanel1.RowStyles.Insert(tableLayoutPanel1.RowCount-1, newRow);
            // Create a panel with the text and the button for deleting the entry.
            System.Windows.Forms.Panel newPanel = new System.Windows.Forms.Panel() {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Tag = url
            };
            newPanel.Click += (obj, e) => {
                tc.AddTab();
                tc.AddTabContent((string)((System.Windows.Forms.Panel)obj).Tag);
            }; 
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button() {
                Text = "X",
                Anchor = AnchorStyles.Right,
                BackColor = Color.White,
                Location = new Point(162, 35),
                Size = new Size(32, 32),
                Tag = Guid.NewGuid(),
                Name = date
            };
            btn.Click += new EventHandler((obj, e) => {
                dataController.RemoveFromDB(((System.Windows.Forms.Button)obj).Name, "date", DBTables.History);                
                tableLayoutPanel1.Controls.Remove(
                    tableLayoutPanel1.GetControlFromPosition(0, dict[(Guid)((System.Windows.Forms.Button)obj).Tag]) );
            });
            dict.Add((Guid)btn.Tag, rowCounter++);
            newPanel.Controls.Add(btn);
            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label() {
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Location = new Point(6, 11),
                Font = new Font(FontFamily.GenericSansSerif, 11f),
                Text = $"{date} - {url}",
                Tag = url
            };
            lbl.Click += (obj, e) => {
                    tc.AddTab();
                    tc.AddTabContent((string)((System.Windows.Forms.Label)obj).Tag);
            };
            newPanel.Controls.Add(lbl);            
            // Adding the row to the History panel.
            tableLayoutPanel1.Controls.Add(newPanel, 0, tableLayoutPanel1.RowCount-1);
            tableLayoutPanel1.RowCount++;
        }



    }
}
