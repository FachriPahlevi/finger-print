using DPUruNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FingerPrint4
{
    public partial class FormFingerPrintLogin : Form
    {
        private ReaderCollection readers;
        private Reader currentReader;
        private CaptureResult captureResult;
        private Form parent;
        private readonly UserRepository userRepository;

        public FormFingerPrintLogin()
        {
            InitializeComponent();
            userRepository = new UserRepository();
        }

        public FormFingerPrintLogin(ReaderCollection readers, Reader currentReader, CaptureResult captureResult, Form parent)
        {
            InitializeComponent();
            userRepository = new UserRepository();
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

                        List<UserFingerprint> users = userRepository.GetUsersWithFingerprints();

                        if (users.Count == 0)
                        {
                            lblStatus.Text =
                                AppMessages.FingerprintNotRegistered;

                            if (parent != null)
                            {
                                btnBack.Visible = true;
                            }

                            return;
                        }

                        List<Fmd> fmds =
                            users.Select(x => x.Fmd)
                            .ToList();

                        IdentifyResult identifyResult =
                            Comparison.Identify(
                                scannedFmd,
                                0,
                                fmds,
                                int.MaxValue / 100000,
                                1
                            );

                        if (identifyResult.ResultCode ==
                            Constants.ResultCode.DP_SUCCESS
                            &&
                            identifyResult.Indexes.Length > 0)
                        {
                            int matchedIndex =
                                identifyResult.Indexes[0][0];

                            UserFingerprint user =
                                users[matchedIndex];

                            lblStatus.Text =
                                "Fingerprint Match";

                            this.Hide();
                            new FormLoginWithPassword(null, user).ShowDialog(); 
                        }
                        else
                        {
                            lblStatus.Text =
                                AppMessages.FingerprintNotRegistered;

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

        private Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            Bitmap bmp = new Bitmap(
                width,
                height,
                PixelFormat.Format8bppIndexed);

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
                    currentReader.On_Captured -= Reader_OnCaptured;
                    currentReader.CancelCapture();
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
