using MangaCleaner.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MangaCleaner
{
    public class ImagePreprocessor
    {
        private readonly int Black = (0 << 16)   // R
                          | (0 << 8)             // G
                          | (0 << 0);            // B
        private readonly int White = (255 << 16) // R
                          | (255 << 8)           // G
                          | (255 << 0);          // B
        unsafe public void FlattenImage(WriteableBitmap Image)
        {
            int Red;
            int Green;
            int Blue;
            int Brightness;

            var pBackBuffer = Image.BackBuffer;
            var pBuffer = (byte*)pBackBuffer.ToPointer();
            var BufferSize = CalculateBackBufferSize(Image);

            Image.Lock();
            for (int i = 0; i < BufferSize; i += 4)
            {
                Red = pBuffer[i];
                Green = pBuffer[i + 1];
                Blue = pBuffer[i + 2];
                Brightness = (Red + Green + Blue) / 3;
                (*(int*)pBackBuffer) = Brightness < Constants.FLATTENING_THRESHHOLD ? Black : White;
                pBackBuffer += 4;
            }
            Image.AddDirtyRect(new System.Windows.Int32Rect(0, 0, Image.PixelWidth, Image.PixelHeight));
            Image.Unlock();
            PixelGrouping(Image);
        }

        unsafe private void PixelGrouping(WriteableBitmap image)
        {
            if(Constants.GROUPING_MAX_DISTANCE == 0) 
                return; 
            var ImageWrapper = new ImageAccessor((byte*)image.BackBuffer.ToPointer(), image.BackBufferStride);
            image.Lock();
            for (int x = 0; x + Constants.GROUPING_MAX_DISTANCE < image.PixelWidth; x++)
            {
                for (int y = 0; y + Constants.GROUPING_MAX_DISTANCE < image.PixelHeight; y++)
                {
                    var color = ImageWrapper.GetPixel(x, y);
                    for (int n = 0; n < Constants.GROUPING_MAX_DISTANCE; n++)
                    {
                        if (ImageWrapper.GetPixel(x + n, y) == Colors.Black)
                        {
                            ImageWrapper.SetPixel(x, y, Colors.Black);
                            n = Constants.GROUPING_MAX_DISTANCE;
                        }
                        if (ImageWrapper.GetPixel(x, y + n) == Colors.Black)
                        {
                            ImageWrapper.SetPixel(x, y, Colors.Black);
                            n = Constants.GROUPING_MAX_DISTANCE;
                        }
                    }
                }
            };
            image.AddDirtyRect(new System.Windows.Int32Rect(0, 0, image.PixelWidth, image.PixelHeight));
            image.Unlock();
        }

        unsafe private int CalculateBackBufferSize(WriteableBitmap bitmap)
        {
            return bitmap.PixelHeight * bitmap.BackBufferStride ;
        }

    }
}
