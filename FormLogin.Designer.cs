using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FingerPrint4
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.cardPanel1 = new System.Windows.Forms.Panel();
            this.panelTBPassword = new System.Windows.Forms.Panel();
            this.btnShowPassword = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panelTBUsername = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtUsername1 = new System.Windows.Forms.TextBox();
            this.btnLogin1 = new System.Windows.Forms.Button();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.picFingerprint1 = new System.Windows.Forms.PictureBox();
            this.cardPanel1.SuspendLayout();
            this.panelTBPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelTBUsername.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFingerprint1)).BeginInit();
            this.SuspendLayout();
            // 
            // cardPanel1
            // 
            this.cardPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(75)))));
            this.cardPanel1.Controls.Add(this.panelTBPassword);
            this.cardPanel1.Controls.Add(this.panelTBUsername);
            this.cardPanel1.Controls.Add(this.btnLogin1);
            this.cardPanel1.Controls.Add(this.lblTitle1);
            this.cardPanel1.Controls.Add(this.picFingerprint1);
            this.cardPanel1.Location = new System.Drawing.Point(50, 80);
            this.cardPanel1.Name = "cardPanel1";
            this.cardPanel1.Size = new System.Drawing.Size(380, 500);
            this.cardPanel1.TabIndex = 0;
            // 
            // panelTBPassword
            // 
            this.panelTBPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.panelTBPassword.Controls.Add(this.btnShowPassword);
            this.panelTBPassword.Controls.Add(this.pictureBox2);
            this.panelTBPassword.Controls.Add(this.txtPassword);
            this.panelTBPassword.Location = new System.Drawing.Point(50, 319);
            this.panelTBPassword.Name = "panelTBPassword";
            this.panelTBPassword.Size = new System.Drawing.Size(280, 39);
            this.panelTBPassword.TabIndex = 6;
            // 
            // btnShowPassword
            // 
            this.btnShowPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.btnShowPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.btnShowPassword.Image = ((System.Drawing.Image)(resources.GetObject("btnShowPassword.Image")));
            this.btnShowPassword.Location = new System.Drawing.Point(239, 7);
            this.btnShowPassword.Name = "btnShowPassword";
            this.btnShowPassword.Size = new System.Drawing.Size(36, 23);
            this.btnShowPassword.TabIndex = 8;
            this.btnShowPassword.UseVisualStyleBackColor = false;
            this.btnShowPassword.Click += new System.EventHandler(this.btnShowPassword_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.pictureBox2.Image = global::FingerPrint4.Properties.Resources.password_icon;
            this.pictureBox2.Location = new System.Drawing.Point(5, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 22);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Enabled = false;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(33, 7);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 22);
            this.txtPassword.TabIndex = 3;
            // 
            // panelTBUsername
            // 
            this.panelTBUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.panelTBUsername.Controls.Add(this.pictureBox1);
            this.panelTBUsername.Controls.Add(this.txtUsername1);
            this.panelTBUsername.Location = new System.Drawing.Point(50, 261);
            this.panelTBUsername.Name = "panelTBUsername";
            this.panelTBUsername.Size = new System.Drawing.Size(280, 39);
            this.panelTBUsername.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.pictureBox1.Image = global::FingerPrint4.Properties.Resources.user_icon;
            this.pictureBox1.Location = new System.Drawing.Point(5, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // txtUsername1
            // 
            this.txtUsername1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.txtUsername1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername1.Enabled = false;
            this.txtUsername1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUsername1.ForeColor = System.Drawing.Color.White;
            this.txtUsername1.Location = new System.Drawing.Point(33, 8);
            this.txtUsername1.Name = "txtUsername1";
            this.txtUsername1.Size = new System.Drawing.Size(242, 22);
            this.txtUsername1.TabIndex = 2;
            // 
            // btnLogin1
            // 
            this.btnLogin1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.btnLogin1.FlatAppearance.BorderSize = 0;
            this.btnLogin1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin1.ForeColor = System.Drawing.Color.White;
            this.btnLogin1.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin1.Image")));
            this.btnLogin1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin1.Location = new System.Drawing.Point(50, 400);
            this.btnLogin1.Name = "btnLogin1";
            this.btnLogin1.Size = new System.Drawing.Size(280, 50);
            this.btnLogin1.TabIndex = 4;
            this.btnLogin1.Text = "   Login with fingerprint";
            this.btnLogin1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogin1.UseVisualStyleBackColor = false;
            this.btnLogin1.Click += new System.EventHandler(this.BtnLogin1_Click);
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle1.ForeColor = System.Drawing.Color.White;
            this.lblTitle1.Location = new System.Drawing.Point(149, 173);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(89, 37);
            this.lblTitle1.TabIndex = 1;
            this.lblTitle1.Text = "Login";
            // 
            // picFingerprint1
            // 
            this.picFingerprint1.Image = global::FingerPrint4.Properties.Resources.fingerprint;
            this.picFingerprint1.Location = new System.Drawing.Point(120, 30);
            this.picFingerprint1.Name = "picFingerprint1";
            this.picFingerprint1.Size = new System.Drawing.Size(140, 140);
            this.picFingerprint1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFingerprint1.TabIndex = 0;
            this.picFingerprint1.TabStop = false;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(484, 661);
            this.Controls.Add(this.cardPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fingerprint Login";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.cardPanel1.ResumeLayout(false);
            this.cardPanel1.PerformLayout();
            this.panelTBPassword.ResumeLayout(false);
            this.panelTBPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelTBUsername.ResumeLayout(false);
            this.panelTBUsername.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFingerprint1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox picFingerprint1;
        private Panel cardPanel1;
        private Label lblTitle1;
        private TextBox txtUsername1;
        private Button btnLogin1;
        private Panel panelTBUsername;
        private Panel panelTBPassword;
        private TextBox txtPassword;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button btnShowPassword;
    }
}