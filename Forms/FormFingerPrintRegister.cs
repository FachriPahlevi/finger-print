using DPUruNet;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FingerPrint4
{
    public partial class FormFingerPrintRegister : Form
    {
        private ReaderCollection readers;
        private Reader currentReader;
        private CaptureResult captureResult;
        private UserFingerprint user;
        private Form parent;
        private readonly UserRepository userRepository;
        
        public FormFingerPrintRegister()
        {
            InitializeComponent();
            userRepository = new UserRepository();
        }

        public FormFingerPrintRegister(ReaderCollection readers, Reader currentReader, CaptureResult captureResult, UserFingerprint user, Form parent)
        {
            InitializeComponent();
            userRepository = new UserRepository();
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

                    Bitmap bitmap =
                        CreateBitmap(
                            captureResult.Data.Views[0].RawImage,
                            captureResult.Data.Views[0].Width,
                            captureResult.Data.Views[0].Height
                        );

                    pictureFingerprint.Image = bitmap;

                    if (InsertToDatabase())
                    {
                        parent?.Close();
                        Hide();
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

        private void FormFingerPrintRegister_Load(object sender, EventArgs e)
        {
            StartCapture();
        }

        private void FormFingerPrintRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseReader();
        }

        private bool InsertToDatabase()
        {
            try
            {
                DataResult<Fmd> fmdResult =
                    FeatureExtraction.CreateFmdFromFid(
                        captureResult.Data,
                        Constants.Formats.Fmd.ANSI
                    );

                if (fmdResult.ResultCode !=
                    Constants.ResultCode.DP_SUCCESS)
                {
                    MessageBox.Show("Failed create fingerprint template");
                    return false;
                }

                Fmd fmd = fmdResult.Data;

                user.Fmd = fmd;
                userRepository.Add(user);

                MessageBox.Show(
                    "Data saved successfully",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
