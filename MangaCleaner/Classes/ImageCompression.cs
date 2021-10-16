using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MangaCleaner.Classes
{
    /* 
     * If compression reduces image dimensions at some point, make sure that the coordinates after calling OCR are corrected for the original image.
     */
    static class ImageCompression
    {
        /// <summary>
        /// Compresses an image with continually worse quality until, it's smaller than the target filesize.
        /// If you read this and can come up with a cool, math-heavy way to compress to targetsize, change the implementation.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="allowedSize"></param>
        /// <returns></returns>
        internal static async Task<string> CompressImageFile(string file, double allowedSize)
        {
            if (OriginalIsSmallEnough(file, allowedSize))
                return file;
            string tempFile = string.Empty;
            FileStream result = null;
            var original = BitmapFrame.Create(new BitmapImage(new Uri(file)));
            JpegBitmapEncoder encoder;
            var quality = 100;
            while (result is null || result.Length > allowedSize)
            {
                if (!string.IsNullOrEmpty(tempFile))
                {
                    result.Close();
                    File.Delete(tempFile);
                }

                encoder = new JpegBitmapEncoder()
                {
                    QualityLevel = quality
                };
                tempFile = Path.GetTempFileName();
                result = File.Open(tempFile, FileMode.Open);
                encoder.Frames.Add(BitmapFrame.Create(original));
                encoder.Save(result);
                quality -= 3;
            }
            result.Close();
            return tempFile;
        }

        private static bool OriginalIsSmallEnough(string file, double allowedSize)
        {
            var originalFile = new FileInfo(file);
            return originalFile.Length < allowedSize;
        }
    }
}
