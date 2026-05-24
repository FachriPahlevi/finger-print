using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FingerPrint4
{
    public partial class FormFingerPrintRegister : Form
    {
        private String connectionString;
        private ReaderCollection readers;
        private Reader currentReader;
        private CaptureResult captureResult;
        private UserFingerPrint user;
        private Form parent;
        
        public FormFingerPrintRegister()
        {
            InitializeComponent();
        }

        public FormFingerPrintRegister(String connectionString, ReaderCollection readers, Reader currentReader, CaptureResult captureResult, UserFingerPrint user, Form parent)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.readers = readers;
            this.currentReader = currentReader;
            this.captureResult = captureResult;
            this.user = user;
            this.parent = parent;
        }

        private void StartCapture()
        {
            if (readers == null) MessageBox.Show("Readers is null");
            else if (readers.Count != 0)
            {
                try
                {
                    Constants.ResultCode result =
                        currentReader.Open(
                        Constants.CapturePriority.DP_PRIORITY_COOPERATIVE
                    );

                    if (result != Constants.ResultCode.DP_SUCCESS)
                    {
                        MessageBox.Show("Fail start capture again");
                        return;
                    }

                    lblStatus.Text =
                        "Attach fingerprint...";

                    CaptureFinger();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show(
                    "Check Your Fingerprint Connection",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
            }
        }

        private void CaptureFinger()
        {
            try
            {
                currentReader.On_Captured -= Reader_OnCaptured;
                currentReader.On_Captured += Reader_OnCaptured;

                currentReader.CaptureAsync(
                    Constants.Formats.Fid.ANSI,
                    Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT,
                    currentReader.Capabilities.Resolutions[0]
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Reader_OnCaptured(CaptureResult result)
        {
            captureResult = result;

            Invoke(new Action(() =>
            {
                if (captureResult.ResultCode ==
                    Constants.ResultCode.DP_SUCCESS)
                {
                    lblStatus.Text =
                        "Fingerprint successfully read";

                    // TAMPILKAN GAMBAR FINGERPRINT
                    Bitmap bitmap =
                        CreateBitmap(
                            captureResult.Data.Views[0].RawImage,
                            captureResult.Data.Views[0].Width,
                            captureResult.Data.Views[0].Height
                        );

                    //pictureFingerprint.Visible = true;
                    pictureFingerprint.Image = bitmap;

                    InsertToDatabase();

                    this.parent.Close();
                    this.Hide();
                }
                else
                {
                    lblStatus.Text =
                        "Failed to capture fingerprint";
                }
            }));
        }

        // METHOD KONVERSI BYTE[] KE BITMAP
        private Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            Bitmap bmp = new Bitmap(
                width,
                height,
                PixelFormat.Format8bppIndexed);

            // grayscale palette
            ColorPalette palette = bmp.Palette;

            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }

            bmp.Palette = palette;

            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int stride = bmpData.Stride;

            // copy per line
            for (int y = 0; y < height; y++)
            {
                Marshal.Copy(
                    bytes,
                    y * width,
                    ptr + y * stride,
                    width);
            }

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        private void CloseReader()
        {
            try
            {
                if (currentReader != null)
                {
                    // Hapus event
                    currentReader.On_Captured -= Reader_OnCaptured;

                    // Cancel capture
                    currentReader.CancelCapture();

                    // Dispose reader
                    currentReader.Dispose();

                    currentReader = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormFingerPrintRegister_Load(object sender, EventArgs e)
        {
            StartCapture();
        }

        private void FormFingerPrintRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseReader();
        }

        private void InsertToDatabase()
        {
            string name = user.Name;
            string password = user.Password;

            try
            {
                // =========================
                // CONVERT FINGERPRINT TO FMD
                // =========================

                DataResult<Fmd> fmdResult =
                    FeatureExtraction.CreateFmdFromFid(
                        captureResult.Data,
                        Constants.Formats.Fmd.ANSI
                    );

                if (fmdResult.ResultCode !=
                    Constants.ResultCode.DP_SUCCESS)
                {
                    MessageBox.Show("Failed create fingerprint template");
                    return;
                }

                Fmd fmd = fmdResult.Data;

                string fingerprintXml = Fmd.SerializeXml(fmd);

                using (SqlConnection con =
                    new SqlConnection(connectionString))
                {
                    con.Open();
                    // =========================
                    // INSERT USER
                    // =========================

                    string insertSql =
                        @"INSERT INTO Users
                            (Name, FingerPrint, Password)
                            VALUES
                            (@Name, @FingerPrint, @Password)";

                    SqlCommand insertCmd =
                        new SqlCommand(insertSql, con);

                    insertCmd.Parameters.AddWithValue(
                        "@Name",
                        name
                    );

                    insertCmd.Parameters.AddWithValue(
                        "@FingerPrint",
                        fingerprintXml
                    );

                    insertCmd.Parameters.AddWithValue(
                        "@Password",
                        DatabaseConnection.Encrypt(password)
                    );

                    insertCmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    "Data saved successfully",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
