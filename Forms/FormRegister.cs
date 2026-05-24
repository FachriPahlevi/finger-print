using DPUruNet;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FingerPrint4
{
    public partial class FormRegister : Form
    {
        private ReaderCollection readers;
        private Reader currentReader;
        private CaptureResult captureResult;
        private readonly UserRepository userRepository;

        bool showPassword;
        public FormRegister()
        {
            InitializeComponent();

            userRepository = new UserRepository();
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
                    MessageBox.Show(AppMessages.FingerprintNotDetected, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string name = txtUsername1.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(
                    AppMessages.UserNameRequired,
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    AppMessages.PasswordRequired,
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            try
            {
                if (userRepository.ExistsByName(name))
                {
                    MessageBox.Show(
                        AppMessages.UserAlreadyExists,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            FormFingerPrintRegister formFingerPrintRegister =
                new FormFingerPrintRegister(readers, currentReader, captureResult, new UserFingerprint(name, password), this);
            
            this.Hide();
            formFingerPrintRegister.ShowDialog();
        }

        private void FormRegister_FormClosing(object sender, FormClosingEventArgs e)
        { }
    }
}
