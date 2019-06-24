using ImageHandler.Klassen;
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
        public bool EndReached = false;
        public Queue<LockBitmap> Imagebuffer = new Queue<LockBitmap>();

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
            string savepath;
            if (Constants.SAVEPATH != "default") {
                savepath = Constants.SAVEPATH;
            }
            else
            {
                savepath = Directory.GetCurrentDirectory() + @"\results";
            }
                
            if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);

            if (Constants.MakeTrainingData)
                SaveResultForTraining(savepath);
            savepath += "\\" + FileIndex.ToString() + ".png";
            bitmap.Save(savepath, ImageFormat.Png);
        }

        private void SaveResultForTraining(string savepath)
        {
             CheckCreateFilesExist(savepath);

            if (SpeechBubble.BoundingBoxes.Count == 0)
            {
                File.Copy(getCurrentfile(), savepath + @"\negatives\" + FileIndex.ToString() + ".png", true);
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(savepath + @"\bg.txt" , true))
                {
                    file.WriteLine(@"negatives\" + FileIndex.ToString() + ".png");
                }
            }
            if (SpeechBubble.BoundingBoxes.Count > 0)
            {
                File.Copy(getCurrentfile(), savepath + @"\positives\" + FileIndex.ToString() + ".png", true);
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(savepath + @"\info.dat" , true))
                {
                    string Line = savepath + FileIndex.ToString() + ".png   " + SpeechBubble.BoundingBoxes.Count.ToString();
                    foreach (Rectangle rect in SpeechBubble.BoundingBoxes)
                    {
                        Line += "    " + rect.X + " " + rect.Y + " " + rect.Width + " " + rect.Height;
                    }
                    file.WriteLine(Line);
                }
            }
        }

        private void CheckCreateFilesExist(string savepath)
        {
            List<string> files = new List<string> { savepath + @"\bg.txt", savepath + @"\info.dat", savepath + @"\positives", savepath + @"\negatives" };
            if(!Directory.Exists(savepath + @"\positives"))
                Directory.CreateDirectory(savepath + @"\positives");
            if (!Directory.Exists(savepath + @"\negatives"))
                Directory.CreateDirectory(savepath + @"\negatives");
            if (!File.Exists(savepath + @"\bg.txt"))
                (File.Create(savepath + @"\bg.txt")).Close();
            if (!File.Exists(savepath + @"\info.dat"))
                (File.Create(savepath + @"\info.dat")).Close();
        }

    }
}
