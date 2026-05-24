using System.Drawing;
using System.Windows.Forms;

namespace FingerPrint4
{
    partial class FormFingerPrintLogin
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
            this.pictureFingerprint = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.picFingerprint1 = new System.Windows.Forms.PictureBox();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFingerprint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFingerprint1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureFingerprint
            // 
            this.pictureFingerprint.Location = new System.Drawing.Point(270, 109);
            this.pictureFingerprint.Name = "pictureFingerprint";
            this.pictureFingerprint.Size = new System.Drawing.Size(260, 226);
            this.pictureFingerprint.TabIndex = 6;
            this.pictureFingerprint.TabStop = false;
            this.pictureFingerprint.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(12, 355);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(776, 51);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "FingerPrint Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picFingerprint1
            // 
            this.picFingerprint1.Image = global::FingerPrint4.Properties.Resources.fingerprint;
            this.picFingerprint1.Location = new System.Drawing.Point(249, 44);
            this.picFingerprint1.Name = "picFingerprint1";
            this.picFingerprint1.Size = new System.Drawing.Size(299, 362);
            this.picFingerprint1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFingerprint1.TabIndex = 4;
            this.picFingerprint1.TabStop = false;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(250, 428);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(298, 50);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "Back";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // FormFingerPrintLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(800, 557);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pictureFingerprint);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.picFingerprint1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormFingerPrintLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fingerprint Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFingerPrintLogin_FormClosing);
            this.Load += new System.EventHandler(this.FormFingerPrintLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureFingerprint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFingerprint1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureFingerprint;
        private Label lblStatus;
        private PictureBox picFingerprint1;
        private Button btnBack;
    }
}