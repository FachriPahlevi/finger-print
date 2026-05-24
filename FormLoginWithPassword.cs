using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FingerPrint4
{
    public partial class FormLoginWithPassword : Form
    {
        private bool showPassword;
        private Form parent;
        private UserFingerPrint user;
        private String connectionString;

        public FormLoginWithPassword(Form parent, UserFingerPrint user = null)
        {
            InitializeComponent();

            showPassword = false;
            setUI();
            this.parent = parent;

            if (user != null)
            {
                txtUsername1.Text = user.Name;
                txtPassword.Text = DatabaseConnection.Decrypt(user.Password);
            }
        }

        private void setUI()
        {
            // Border Radius Panel
            GraphicsPath path = new GraphicsPath();

            int radius = 30;

            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(cardPanel1.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(cardPanel1.Width - radius, cardPanel1.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, cardPanel1.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            cardPanel1.Region = new Region(path);

            // Border Radius Button
            btnLogin1.FlatStyle = FlatStyle.Flat;
            btnLogin1.FlatAppearance.BorderSize = 0;

            GraphicsPath path2 = new GraphicsPath();
            path2.AddArc(0, 0, 20, 20, 180, 90);
            path2.AddArc(btnLogin1.Width - 20, 0, 20, 20, 270, 90);
            path2.AddArc(btnLogin1.Width - 20, btnLogin1.Height - 20, 20, 20, 0, 90);
            path2.AddArc(0, btnLogin1.Height - 20, 20, 20, 90, 90);
            path2.CloseAllFigures();

            btnLogin1.Region = new Region(path2);

            // Border Radius TextBox UserName
            GraphicsPath path3 = new GraphicsPath();

            radius = 20;
            path3.StartFigure();
            path3.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path3.AddArc(new Rectangle(panelTBUsername.Width - radius, 0, radius, radius), 270, 90);
            path3.AddArc(new Rectangle(panelTBUsername.Width - radius, panelTBUsername.Height - radius, radius, radius), 0, 90);
            path3.AddArc(new Rectangle(0, panelTBUsername.Height - radius, radius, radius), 90, 90);
            path3.CloseFigure();

            panelTBUsername.Region = new Region(path3);

            // Border Radius TextBox Password
            GraphicsPath path4 = new GraphicsPath();
            path4.StartFigure();
            path4.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path4.AddArc(new Rectangle(panelTBPassword.Width - radius, 0, radius, radius), 270, 90);
            path4.AddArc(new Rectangle(panelTBPassword.Width - radius, panelTBPassword.Height - radius, radius, radius), 0, 90);
            path4.AddArc(new Rectangle(0, panelTBPassword.Height - radius, radius, radius), 90, 90);
            path4.CloseFigure();

            panelTBPassword.Region = new Region(path4);

            // Icon Button
            //Image resized = new Bitmap(global::FingerPrint4.Properties.Resources.fingerprint_04, new Size(45, 35));
            //btnLogin1.Image = resized;

            Image resized2 = new Bitmap(global::FingerPrint4.Properties.Resources.eye_icon, new Size(25, 15));
            btnShowPassword.Image = resized2;
        }

        private void BtnLogin1_Click(object sender, EventArgs e)
        {
            string name = txtUsername1.Text;
            string password = txtPassword.Text;

            using (SqlConnection con =
                new SqlConnection(connectionString))
            {
                con.Open();

                string sql =
                    @"SELECT * FROM Users
                    WHERE name=@Username
                    AND password=@Password";

                SqlCommand cmd =
                    new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue(
                    "@Username",
                    name
                );

                cmd.Parameters.AddWithValue(
                    "@Password",
                    DatabaseConnection.Encrypt(password)
                );

                SqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    FormDashboard frm =
                        new FormDashboard();

                    this.Hide();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show(
                        "Login Failed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information
                    );
                }
            }
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            showPassword = !showPassword;
            Image img = (!showPassword) ? global::FingerPrint4.Properties.Resources.eye_slash : global::FingerPrint4.Properties.Resources.eye_icon;
            Image resized = new Bitmap(img, new Size(25, 15));

            btnShowPassword.Image = resized;

            txtPassword.UseSystemPasswordChar = (!showPassword);
        }

        private void FormLoginWithPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (parent!=null) parent.Close();
        }

        private void FormLoginWithPassword_Load(object sender, EventArgs e)
        {
            this.connectionString = DatabaseConnection.connectionString;
        }
    }
}
