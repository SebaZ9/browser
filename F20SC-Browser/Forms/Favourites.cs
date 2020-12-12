using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F20SC_Browser
{
    public partial class Favourites: Form
    {

        UserData userData;
        DataController dc;
        string previousCellValue;


        public Favourites()
        {
            InitializeComponent();
            // Gets user's favourites and inserts them into the grid view.
            userData = UserData.userData;
            dc = new DataController();
            dataGridView1.DataSource = dc.GetRowsData(DBTables.Favourites);

            //Set up the Data Grid View
            dataGridView1.Columns["id"].ReadOnly = true;
            dataGridView1.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["id"].Width = 30;
            dataGridView1.Columns.Add(new DataGridViewColumn() {
                CellTemplate = new DataGridViewTextBoxCell() { },
                HeaderText = "Open Page",
                Width = 40,
                MinimumWidth = 40,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.Green }
            });
            dataGridView1.Columns.Add(new DataGridViewColumn() {
                CellTemplate = new DataGridViewTextBoxCell(),
                HeaderText = "Delete",
                Width = 40,
                MinimumWidth = 40,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.Red }
            });
            dataGridView1.CellValueChanged += CellValueChanged;
        }

        // Event for clicking the X to remove a favourite
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            if(e.ColumnIndex == 1 && e.RowIndex >= 0) {     
                userData.RemoveFromFavourites((string)dataGridView1[3, e.RowIndex].Value);
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
            if(e.ColumnIndex == 0) {
                TabController.tc.AddTab();
                TabController.tc.AddTabContent((string)dataGridView1[3, e.RowIndex].Value);
            }
        }

        private void CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            // If URL
            if (e.ColumnIndex == 3) {

                string newURL = (string)dataGridView1[3, e.RowIndex].Value;
                if (Uri.IsWellFormedUriString(newURL, UriKind.Absolute)) {
                    userData.ModifyFavourite(previousCellValue, (string)dataGridView1[3, e.RowIndex].Value, (string)dataGridView1[4, e.RowIndex].Value);
                } else {
                    dataGridView1[3, e.RowIndex].Value = previousCellValue;
                    MessageBox.Show("URL is not well formed.", "Error", MessageBoxButtons.OK);
                }

            }
            // If nick name
            if (e.ColumnIndex == 4) {
                userData.ModifyFavourite((string)dataGridView1[3, e.RowIndex].Value, (string)dataGridView1[3, e.RowIndex].Value, (string)dataGridView1[4, e.RowIndex].Value);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e) {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4) {
                previousCellValue = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
            }
        }
    }
}
