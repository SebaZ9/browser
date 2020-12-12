using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F20SC_Browser {
    public partial class Settings : Form {

        UserData userData;

        public Settings() {
            InitializeComponent();
            userData = UserData.userData;
            textBox1.Text = userData.GetHomePage();

        }

        private void btnSetHomePage(object sender, EventArgs e) {
            userData.SetHomePage(textBox1.Text);
        }

    }
}
