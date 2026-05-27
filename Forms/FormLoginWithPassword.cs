using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FingerPrint4
{
    public partial class FormLoginWithPassword : Form
    {
        private bool showPassword;
        private Form parent;
        private readonly UserRepository userRepository;

        public FormLoginWithPassword(Form parent, UserFingerprint user = null)
        {
            InitializeComponent();

            userRepository = new UserRepository();
            showPassword = false;
            setUI();
            this.parent = parent;

            if (user != null)
            {
                txtUsername1.Text = user.Name;
                txtPassword.Text = PasswordProtector.Decrypt(user.Password);
            }
        }

        private void setUI()
        {
            GraphicsPath path = new GraphicsPath();

            int radius = 30;

            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(cardPanel1.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(cardPanel1.Width - radius, cardPanel1.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, cardPanel1.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            cardPanel1.Region = new Region(path);

            btnLogin1.FlatStyle = FlatStyle.Flat;
            btnLogin1.FlatAppearance.BorderSize = 0;

            GraphicsPath path2 = new GraphicsPath();
            path2.AddArc(0, 0, 20, 20, 180, 90);
            path2.AddArc(btnLogin1.Width - 20, 0, 20, 20, 270, 90);
            path2.AddArc(btnLogin1.Width - 20, btnLogin1.Height - 20, 20, 20, 0, 90);
            path2.AddArc(0, btnLogin1.Height - 20, 20, 20, 90, 90);
            path2.CloseAllFigures();

            btnLogin1.Region = new Region(path2);

            GraphicsPath path3 = new GraphicsPath();

            radius = 20;
            path3.StartFigure();
            path3.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path3.AddArc(new Rectangle(panelTBUsername.Width - radius, 0, radius, radius), 270, 90);
            path3.AddArc(new Rectangle(panelTBUsername.Width - radius, panelTBUsername.Height - radius, radius, radius), 0, 90);
            path3.AddArc(new Rectangle(0, panelTBUsername.Height - radius, radius, radius), 90, 90);
            path3.CloseFigure();

            panelTBUsername.Region = new Region(path3);

            GraphicsPath path4 = new GraphicsPath();
            path4.StartFigure();
            path4.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path4.AddArc(new Rectangle(panelTBPassword.Width - radius, 0, radius, radius), 270, 90);
            path4.AddArc(new Rectangle(panelTBPassword.Width - radius, panelTBPassword.Height - radius, radius, radius), 0, 90);
            path4.AddArc(new Rectangle(0, panelTBPassword.Height - radius, radius, radius), 90, 90);
            path4.CloseFigure();

            panelTBPassword.Region = new Region(path4);

            Image resized2 = new Bitmap(global::FingerPrint4.Properties.Resources.eye_icon, new Size(25, 15));
            btnShowPassword.Image = resized2;
        }

        private void BtnLogin1_Click(object sender, EventArgs e)
        {
            string name = txtUsername1.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(AppMessages.LoginFailed, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (userRepository.IsValidCredential(name, password))
                {
                    FormDashboard frm = new FormDashboard();

                    this.Hide();
                    frm.ShowDialog();
                    return;
                }

                MessageBox.Show(AppMessages.LoginFailed, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }
}
