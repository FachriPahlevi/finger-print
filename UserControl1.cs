using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrint4
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister form1 = new FormRegister();
            form1.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormLogin form1 = new FormLogin();
            form1.ShowDialog();
        }
    }
}
