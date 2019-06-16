using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace ImageHandler
{
    class ImageBuffer
    {
        public static ImageHandler ImageHandler;
        public Queue<LockBitmap> Imagebuffer = new Queue<LockBitmap>();
        public bool EndReached = false;

        private string FileDialogFileName;
        private string[] filevector;
        private int FileIndex = 0;

        public static event Action<int> BufferSizeChanged;

        /// <summary>
        /// Windows function for sorting strings like in the fielsystem
        /// </summary>
        /// <param name="psz1"></param>
        /// <param name="psz2"></param>
        /// <returns></returns>
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);

        public ImageBuffer(ImageHandler ImageHandlerReference ,string FileSource)
        {
            ImageHandler = ImageHandlerReference;
            FileDialogFileName = FileSource;
        }
        public void FillBuffer()
        {
            filevector = Directory.GetFiles(Directory.GetCurrentDirectory());
            Array.Sort(filevector,StrCmpLogicalW);
            for (int i = 0; i < filevector.Length; i++)
            {
                if (filevector[i] == FileDialogFileName)
                    FileIndex = i;
            }

            int iterator = FileIndex;
            while (!EndReached)
            {
                if (Imagebuffer.Count < Constants.BUFFERSIZE && filevector.Length > FileIndex + Imagebuffer.Count)
                {
                    try
                    {
                        Bitmap editable = new Bitmap(filevector[iterator]);
                        Imagebuffer.Enqueue(ImageHandler.preProcessing(new Bitmap(editable)));
                        BufferSizeChanged(Imagebuffer.Count);
                        iterator++;
                    }
                    catch (Exception)
                    {
                        BufferSizeChanged(-1);
                        EndReached = true;
                    }
                }
            }
        }
        public string getNextFile()
        {
            FileIndex++;
            return filevector[FileIndex];
        }
        public string getCurrentfile()
        {
            return filevector[FileIndex];
        }
        public LockBitmap getNextImage()
        {
            while (Imagebuffer.Count == 0) { }
            BufferSizeChanged(Imagebuffer.Count - 1);
            return Imagebuffer.Dequeue();
        }
        public void Save(Bitmap bitmap)
        {
            string savepath = Directory.GetCurrentDirectory() + @"\results";
            if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);
            savepath += "\\" + FileIndex.ToString() + ".png";
            bitmap.Save(savepath, ImageFormat.Png);
        }
    }
}
