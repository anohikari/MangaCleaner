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
    static class ImageCompression
    {
        static async Task<FileStream> CompressImageFile(string file)
        {
            var tempFile = Path.GetTempFileName();
            var original = BitmapFrame.Create(new BitmapImage(new Uri(file)));
            var encoder = new JpegBitmapEncoder();
            var result = new FileStream(tempFile, FileMode.Create);
            encoder.Frames.Add(BitmapFrame.Create(original));
            encoder.Save(result);
            return result;
        }

        static double BitsPerPixel(BitmapFrame bitmap, string sourceFile)
        {
            var pixel = bitmap.Width * bitmap.Height;
            var size = new FileInfo(sourceFile).Length;
            return  size / pixel;
        }

        static Point NewFileFormat(BitmapFrame bitmap, string sourceFile, double targetImageSize = 1000000)
        {
            var currentFileSize = new FileInfo(sourceFile).Length;
            var sizerReductionFactor = targetImageSize / currentFileSize;
            return new Point(0, 0);
        }
    }
}
