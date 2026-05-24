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
    public partial class FormRegister : Form
    {
        private String connectionString;
        private ReaderCollection readers;
        private Reader currentReader;
        private CaptureResult captureResult;

        bool showPassword;
        public FormRegister()
        {
            InitializeComponent();

            showPassword = false;
            setUI();
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
            btnRegister1.FlatStyle = FlatStyle.Flat;
            btnRegister1.FlatAppearance.BorderSize = 0;

            GraphicsPath path2 = new GraphicsPath();
            path2.AddArc(0, 0, 20, 20, 180, 90);
            path2.AddArc(btnRegister1.Width - 20, 0, 20, 20, 270, 90);
            path2.AddArc(btnRegister1.Width - 20, btnRegister1.Height - 20, 20, 20, 0, 90);
            path2.AddArc(0, btnRegister1.Height - 20, 20, 20, 90, 90);
            path2.CloseAllFigures();

            btnRegister1.Region = new Region(path2);

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
            Image resized = new Bitmap(global::FingerPrint4.Properties.Resources.fingerprint_04, new Size(45, 35));
            btnRegister1.Image = resized;

            Image resized2 = new Bitmap(global::FingerPrint4.Properties.Resources.eye_icon, new Size(25, 15));
            btnShowPassword.Image = resized2;
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            showPassword = !showPassword;
            Image img = (!showPassword) ? global::FingerPrint4.Properties.Resources.eye_slash : global::FingerPrint4.Properties.Resources.eye_icon;
            Image resized = new Bitmap(img, new Size(25, 15));
            
            btnShowPassword.Image = resized;

            txtPassword.UseSystemPasswordChar = (!showPassword);
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            this.InitReader();
            this.connectionString = DatabaseConnection.connectionString;
        }
        
        private void InitReader()
        {
            try
            {
                ReaderCollection readers = ReaderCollection.GetReaders();

                if (readers.Count > 0)
                {
                    currentReader = readers[0];
                    this.readers = readers;
                    btnRegister1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
                    btnRegister1.Enabled = true;
                }
                else
                {
                    btnRegister1.BackColor = Color.Silver;
                    btnRegister1.Enabled= false;
                    MessageBox.Show("Fingerprint not detected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.readers = readers;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnRegister1_Click(object sender, EventArgs e)
        {
            string Name = txtUsername1.Text;
            string Password = txtPassword.Text;
            FormFingerPrintRegister formFingerPrintRegister =
                new FormFingerPrintRegister(connectionString, readers, currentReader, captureResult, new UserFingerPrint(Name, Password),this);
            string name = txtUsername1.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(
                    "You must insert name",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "You must insert password",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            try
            {
                // =========================
                // CHECK USER EXISTS
                // =========================

                bool userExist = false;

                using (SqlConnection con =
                    new SqlConnection(connectionString))
                {
                    con.Open();

                    string checkSql =
                        "SELECT * FROM Users WHERE Name=@Name";

                    SqlCommand checkCmd =
                        new SqlCommand(checkSql, con);

                    checkCmd.Parameters.AddWithValue(
                        "@Name",
                        name
                    );

                    SqlDataReader reader =
                        checkCmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userExist = true;
                    }

                    reader.Close();

                    if (userExist)
                    {
                        MessageBox.Show(
                            "User already exists",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            this.Hide();
            formFingerPrintRegister.ShowDialog();
        }

        private void FormRegister_FormClosing(object sender, FormClosingEventArgs e)
        { }
    }
}
