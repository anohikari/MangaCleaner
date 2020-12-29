using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace MangaCleaner
{
    class ImagePreprocessor
    {
        private int Black = (0 << 16)   // R
                          | (0 << 8)    // G
                          | (0 << 0);   // B
        private int White = (255 << 16) // R
                          | (255 << 8)  // G
                          | (255 << 0); // B
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
        }

        unsafe private int CalculateBackBufferSize(WriteableBitmap bitmap)
        {
            return bitmap.PixelHeight * bitmap.BackBufferStride ;
        }

    }
}
