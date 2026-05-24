using System.Drawing;
using System.Windows.Forms;

namespace FingerPrint4
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Fingerprint Auth";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(800, 450);

            UserControl1 mainMenu = new UserControl1
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(mainMenu);
        }
    }
}
