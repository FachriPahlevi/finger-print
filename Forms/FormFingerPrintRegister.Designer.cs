using System.Drawing;
using System.Windows.Forms;

namespace FingerPrint4
{
    partial class FormFingerPrintRegister
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.pictureFingerprint = new System.Windows.Forms.PictureBox();
            this.picFingerprint1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFingerprint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFingerprint1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(233, 351);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(299, 51);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "FingerPrint Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureFingerprint
            // 
            this.pictureFingerprint.Location = new System.Drawing.Point(254, 105);
            this.pictureFingerprint.Name = "pictureFingerprint";
            this.pictureFingerprint.Size = new System.Drawing.Size(260, 226);
            this.pictureFingerprint.TabIndex = 3;
            this.pictureFingerprint.TabStop = false;
            this.pictureFingerprint.Visible = false;
            // 
            // picFingerprint1
            // 
            this.picFingerprint1.Image = global::FingerPrint4.Properties.Resources.fingerprint;
            this.picFingerprint1.Location = new System.Drawing.Point(233, 40);
            this.picFingerprint1.Name = "picFingerprint1";
            this.picFingerprint1.Size = new System.Drawing.Size(299, 362);
            this.picFingerprint1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFingerprint1.TabIndex = 1;
            this.picFingerprint1.TabStop = false;
            // 
            // FormFingerPrintRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(800, 534);
            this.Controls.Add(this.pictureFingerprint);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.picFingerprint1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormFingerPrintRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fingerprint Register";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFingerPrintRegister_FormClosing);
            this.Load += new System.EventHandler(this.FormFingerPrintRegister_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureFingerprint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFingerprint1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Label lblStatus;
        private PictureBox pictureFingerprint;
        private PictureBox picFingerprint1;
    }
}