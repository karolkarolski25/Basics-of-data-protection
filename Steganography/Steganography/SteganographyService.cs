using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Steganography
{
    public static class SteganographyService
    {
        public static Task<string> ExtractHiddenText(Bitmap inputBitmap)
        {
            string msg = string.Empty;
            int textIter = 0, msgLen = inputBitmap.GetPixel(inputBitmap.Width - 1, 0).B;

            for (int i = 0; i < inputBitmap.Width; i++)
            {
                for (int j = 0; j < inputBitmap.Height; j++)
                {

                    if (i % 4 == 0 && textIter < msgLen)
                    {
                        if (j % 4 == 0)
                        {
                            var pixel = inputBitmap.GetPixel(i, j);
                            msg += Encoding.ASCII.GetString(new byte[] { Convert.ToByte(Convert.ToChar(pixel.B)) });
                            textIter++;
                        }
                    }
                }
            }

            return Task.FromResult(msg);
        }

        public static Task<Bitmap> HideText(string textToHide, Bitmap bmp)
        {
            int textIter = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    var pixel = bmp.GetPixel(i, j);

                    if (i % 4 == 0 && textIter < textToHide.Length)
                    {
                        if (j % 4 == 0)
                        {
                            char letterToEncrypt = Convert.ToChar(textToHide.Substring(textIter++, 1));

                            bmp.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, Convert.ToInt32(letterToEncrypt)));
                        }
                    }

                    if (i == bmp.Width - 1 && j == 0)
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, textToHide.Length));
                    }
                }
            }

            return Task.FromResult(bmp);
        }
    }
}
