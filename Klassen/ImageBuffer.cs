using ImageHandler.Klassen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageHandler
{
    class ImageBuffer
    {
        public LockBitmap[] Imagebuffer = new LockBitmap[Constants.BUFFERSIZE];
        int index = 0;
        public int current_buffersize = 0;
        string File;

        string[] filevector;
        int FileIndex = 0;

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);

        public static MyImages images;
        public ImageBuffer(MyImages img,string dir)
        {
            images = img;
            File = dir;
        }
        public void FillBuffer()
        {

            filevector = Directory.GetFiles(Directory.GetCurrentDirectory());
            Array.Sort(filevector,StrCmpLogicalW);
            for (int i = 0; i < filevector.Length; i++)
            {
                if (filevector[i] == File)
                    FileIndex = i;
            }
            
            while (true)
            {
                if (current_buffersize < Constants.BUFFERSIZE && filevector.Length > FileIndex + current_buffersize)
                {
                    Bitmap editable = new Bitmap(filevector[FileIndex + current_buffersize]);
                    Imagebuffer[(index + current_buffersize) % Constants.BUFFERSIZE] = images.preProcessing(new Bitmap(editable));
                    current_buffersize++;
                }
            }

        }
        public string getNextFile()
        {
            FileIndex++;
            return filevector[FileIndex];
        }
        public LockBitmap getCurrentImage()
        {
            return Imagebuffer[index % 5];
        }
        public LockBitmap getNextImage()
        {
            current_buffersize--;
            while(current_buffersize == 0) { }
            index++;
            return Imagebuffer[index % 5];
        }
        public void Save(Bitmap bitmap)
        {
            string p = Directory.GetCurrentDirectory();
            p += @"\results";
            if (!Directory.Exists(p))
                Directory.CreateDirectory(p);
            p += "\\" + index.ToString() + ".png";
            bitmap.Save(p, ImageFormat.Png);
        }
    }
}
