using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Utility
{
    internal class ImageCompress
    {
        internal static Bitmap CompressionJpg(Bitmap bitmap1, long v)
        {
            MemoryStream memoryStream = new MemoryStream();

            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpgEncoder = null;

            foreach (ImageCodecInfo codec in encoders)
            {
                if (codec.FormatID == ImageFormat.Jpeg.Guid)
                {
                    jpgEncoder = codec;
                    break;
                }
            }
            System.Drawing.Imaging.Encoder myEncoder =
            System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(myEncoder, 25L);

            bitmap1.Save(memoryStream, jpgEncoder, myEncoderParameters);
            Bitmap compressBitmap = new Bitmap(memoryStream);
            return compressBitmap;

            //Bitmap
            //MemorySteam

        }

        internal static Bitmap ResizeImage(Bitmap bitmap1, int v1, int v2)
        {
            {
                Bitmap resizedBitmap = new Bitmap(v1, v2);
                Graphics g = Graphics.FromImage(resizedBitmap);

                g.DrawImage(bitmap1, 0, 0, v1, v2);
                return resizedBitmap;
            }

        }
    }
}

