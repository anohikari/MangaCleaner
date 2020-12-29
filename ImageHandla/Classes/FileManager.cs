using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace MangaCleaner
{
    class FileManager
    {
        public bool EndReached = false;

        private string currentDirectory = "";
        private string FileDialogFileName;
        private string[] filevector;
        private int FileIndex = 0;

        public object Current => throw new NotImplementedException();

        public static event Action<int> BufferSizeChanged;

        /// <summary>
        /// Windows function for sorting strings like in the filesystem
        /// </summary>
        /// <param name="psz1"></param>
        /// <param name="psz2"></param>
        /// <returns></returns>
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);

        public FileManager(string FileSource)
        {
            currentDirectory = Path.GetDirectoryName(FileSource);
            FileDialogFileName = FileSource;
            FillBuffer();
        }
        private void FillBuffer()
        {
            filevector = Directory.GetFiles(Path.GetDirectoryName(FileDialogFileName));
            Array.Sort(filevector,StrCmpLogicalW);
            for (int i = 0; i < filevector.Length; i++)
            {
                if (filevector[i] == FileDialogFileName)
                    FileIndex = i;
            }
        }
        public string getNextFile()
        {
            if(FileIndex < filevector.Length - 1)
                FileIndex++;
            return filevector[FileIndex];
        }
        public string getPreviousFile()
        {
            if(FileIndex > 0)
                FileIndex--;
            return filevector[FileIndex];
        }
        public string getCurrentfile()
        {
            return filevector[FileIndex];
        }

        public void Save(WriteableBitmap bitmap)
        {
            string savepath;
            if (Constants.SAVEPATH != "default")
            {
                savepath = Constants.SAVEPATH;
            }
            else
            {
                savepath = currentDirectory + @"\results";
            }

            if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);

            savepath += "\\" + FileIndex.ToString() + ".png";
            using (var fileStream = new FileStream(savepath, FileMode.Create))
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(fileStream);
            }
        }
    }
}
