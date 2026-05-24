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
    public partial class FormFingerPrintLogin : Form
    {
        private String connectionString;
        private ReaderCollection readers;
        private Reader currentReader;
        private CaptureResult captureResult;
        private Form parent;

        public FormFingerPrintLogin()
        {
            InitializeComponent();
        }

        public FormFingerPrintLogin(String connectionString, ReaderCollection readers, Reader currentReader, CaptureResult captureResult, Form parent)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.readers = readers;
            this.currentReader = currentReader;
            this.captureResult = captureResult;
            this.parent = parent;
        }


        private void StartCapture()
        {
            if (readers.Count != 0)
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

                    // =========================
                    // SHOW IMAGE
                    // =========================

                    Bitmap bitmap =
                        CreateBitmap(
                            captureResult.Data.Views[0].RawImage,
                            captureResult.Data.Views[0].Width,
                            captureResult.Data.Views[0].Height
                        );

                    pictureFingerprint.Image =
                        bitmap;

                    try
                    {
                        // =========================
                        // CREATE SCANNED FMD
                        // =========================

                        DataResult<Fmd> scanResult =
                            FeatureExtraction.CreateFmdFromFid(
                                captureResult.Data,
                                Constants.Formats.Fmd.ANSI
                            );

                        if (scanResult.ResultCode !=
                            Constants.ResultCode.DP_SUCCESS)
                        {
                            MessageBox.Show(
                                "Failed create fingerprint template");

                            return;
                        }

                        Fmd scannedFmd =
                            scanResult.Data;

                        // =========================
                        // LOAD ALL USERS
                        // =========================

                        List<UserFingerPrint> users =
                            new List<UserFingerPrint>();

                        using (SqlConnection con =
                            new SqlConnection(connectionString))
                        {
                            con.Open();

                            string sql =
                                "SELECT Name, Password, FingerPrint FROM Users";

                            SqlCommand cmd =
                                new SqlCommand(sql, con);

                            SqlDataReader reader =
                                cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                try
                                {
                                    string fingerprintXml =
                                        reader["FingerPrint"]
                                        .ToString();

                                    Fmd dbFmd =
                                        Fmd.DeserializeXml(
                                            fingerprintXml
                                        );

                                    users.Add(
                                        new UserFingerPrint
                                        {
                                            Name =
                                                reader["Name"]
                                                .ToString(),

                                            Password =
                                                reader["Password"]
                                                .ToString(),

                                            Fmd = dbFmd
                                        });
                                }
                                catch
                                {

                                }
                            }
                        }

                        // =========================
                        // CHECK USER EXISTS
                        // =========================

                        if (users.Count == 0)
                        {
                            lblStatus.Text =
                                "Your fingerprint is not registered";

                            if (parent != null)
                            {
                                btnBack.Visible = true;
                            }

                            return;
                        }

                        // =========================
                        // CREATE FMD LIST
                        // =========================

                        List<Fmd> fmds =
                            users.Select(x => x.Fmd)
                            .ToList();

                        // =========================
                        // IDENTIFY FINGERPRINT
                        // =========================

                        IdentifyResult identifyResult =
                            Comparison.Identify(
                                scannedFmd,
                                0,
                                fmds,
                                int.MaxValue / 100000,
                                1
                            );

                        // =========================
                        // LOGIN SUCCESS
                        // =========================

                        if (identifyResult.ResultCode ==
                            Constants.ResultCode.DP_SUCCESS
                            &&
                            identifyResult.Indexes.Length > 0)
                        {
                            int matchedIndex =
                                identifyResult.Indexes[0][0];

                            UserFingerPrint user =
                                users[matchedIndex];

                            lblStatus.Text =
                                "Fingerprint Match";

                            this.Hide();
                            new FormLoginWithPassword(null, user).ShowDialog(); 
                        }
                        else
                        {
                            lblStatus.Text =
                                "Your fingerprint is not registered";

                            if (parent != null)
                            {
                                btnBack.Visible = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
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

        private void FormFingerPrintLogin_Load(object sender, EventArgs e)
        {
            StartCapture();
        }

        private void FormFingerPrintLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseReader();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.parent.Show();
            this.Hide();
        }
    }
}
