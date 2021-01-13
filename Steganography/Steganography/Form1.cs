using System;
using System.Drawing;
using System.Windows.Forms;

namespace Steganography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                Filter = "All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            try
            {
                var textToHide = TextToHideTextBox.Text;

                if (string.IsNullOrWhiteSpace(textToHide))
                {
                    throw new ArgumentNullException("Text to hide cannot be empty\nTry again");
                }

                if (textToHide.Length > 255)
                {
                    throw new ArgumentOutOfRangeException("Text is too long\nMaximum length is 255 chars\nTry again");
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    Bitmap inputBitmap = new Bitmap(openFileDialog.FileName);

                    InputPictureBox.Image = inputBitmap;
                    InputPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    Bitmap bitmapWithHiddenText = await SteganographyService.HideText(textToHide, inputBitmap);

                    ImageWithText.Image = bitmapWithHiddenText;
                    ImageWithText.SizeMode = PictureBoxSizeMode.StretchImage;

                    bitmapWithHiddenText.Save("BitmapWithText.png");

                    HiddenTextBox.Text = await SteganographyService.ExtractHiddenText(bitmapWithHiddenText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occured during processing\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
