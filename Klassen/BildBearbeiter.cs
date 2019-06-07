using System.Drawing;
using System.Threading.Tasks;

namespace ImageHandler
{
    /// <summary>
    /// Holds all WIP images, and manipulates them
    /// </summary>
    public class MyImages
    {
        ImageBuffer SaveLoad;

        public LockBitmap CurrentImage;         // preprocessed Image
        public LockBitmap ChangeBuffer;         
        public LockBitmap result;               // Latest WIP

        public byte LowerBound = 0;
        public byte UpperBound = 0;


        public void Init(string dir)
        {
            Bitmap toLock = new Bitmap(dir);
            ChangeBuffer = new LockBitmap(new Bitmap(toLock));
            result = new LockBitmap(new Bitmap(toLock));     // result = new bitmap
            SaveLoad = new ImageBuffer(this, dir);
            Task task = new Task(SaveLoad.FillBuffer);
            task.Start();
            while (SaveLoad.current_buffersize == 0) { }
            CurrentImage = SaveLoad.getCurrentImage();
        }
        public void LoadNextimageFromBuffer()
        {
            SaveLoad.Save(result.source);
            while(SaveLoad.current_buffersize == 0) { }
            Bitmap toLock = new Bitmap(SaveLoad.getNextFile());
            ChangeBuffer = new LockBitmap(new Bitmap(toLock));
            result = new LockBitmap(new Bitmap(toLock));
            CurrentImage = SaveLoad.getNextImage();
            
            //Bitmap buffer = new Bitmap(SaveLoad.getNextImage().source);
            //result = ChangeBuffer = CurrentImage = new LockBitmap(new Bitmap(buffer));
            ////result = ChangeBuffer = CurrentImage = SaveLoad.getNextImage();
        }


        /// <summary>
        /// Stretches the colour spectrum between Lowerbound and Upperbound to the full colourspectrum. (linear)
        /// </summary>
        /// <param name="AImage"></param>
        /// <returns></returns>
        public  LockBitmap level(Bitmap AImage)
        {
            byte r, g, b;
            int avg;
            Color result;
            LockBitmap final = new LockBitmap(AImage);
            final.LockBits();

            for (int x = 0; x < AImage.Width; x++)
            {
                for (int y = 0; y < AImage.Height; y++)
                {
                    r = final.GetPixel(x, y).R;
                    g = final.GetPixel(x, y).G;
                    b = final.GetPixel(x, y).B;
                    avg = (r + b + g) / 3;

                    if (r < LowerBound)
                        r = LowerBound;
                    if (g < LowerBound)
                        g = LowerBound;
                    if (b < LowerBound)
                        b = LowerBound;

                    if (r > UpperBound)
                        r = UpperBound;
                    if (g > UpperBound)
                        g = UpperBound;
                    if (b > UpperBound)
                        b = UpperBound;

                    double f = (255.0 / (UpperBound - LowerBound));         
                    result = Color.FromArgb(
                            System.Convert.ToByte(f * (r - LowerBound)),        // linear scaling
                            System.Convert.ToByte(f * (g - LowerBound)),
                            System.Convert.ToByte(f * (b - LowerBound))
                                            );
                    final.SetPixel(x, y, result);
                }
            }
            final.UnlockBits();
            return final;
        }


        /// <summary>
        /// Blurs and flattens the current Image and groups close Pixels to prepare the Image for a Pixel regioning approach
        /// </summary>
        public LockBitmap preProcessing(Bitmap image)
        {
            //byte[] bytesrc = File.ReadAllBytes(openFileDialog.FileName);
            //MemoryStream instream = new MemoryStream(bytesrc);
            //MemoryStream outstream = new MemoryStream();
            //ImageFactory factory = new ImageFactory(preserveExifData: true);
            //factory.Load(instream);
            //factory.GaussianBlur(2);                            // use ImageFactory for the GaussianBlur
            //factory.Save(outstream);                            // Library auf dem server nicht vorhanden

            LockBitmap lockBitmap = new LockBitmap(image);
            lockBitmap.LockBits();

            Color color;
            int avg;
            int h1 = image.Width - 1;
            int h = image.Height - 1;
            //Parallel.For(0, h1, x =>
            for (int x = 0; x < h1; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    color = lockBitmap.GetPixel(x, y);
                    avg = (int)(color.GetBrightness() *255);
                    if (avg < Constants.FLATTENING_THRESHHOLD) 
                        lockBitmap.SetPixel(x, y, Color.Black);
                    else
                        lockBitmap.SetPixel(x, y, Color.White);
                }
            };

            ////Parallel.For(10, h1, x =>         //horizontal and vertical grouping of black pixel
            //for (int x = 0; x < h1; x++)
            //{
            //    for (int y = 0; y < h; y++)
            //    {
            //        color = lockBitmap.GetPixel(x, y);
            //        for (int n = 0; n < Constants.GROUPING_MAX_DISTANCE; n++)
            //        {
            //            if (lockBitmap.GetPixel(x + n, y) == Color.Black)
            //            {
            //                lockBitmap.SetPixel(x, y, Color.Black);
            //                n = Constants.GROUPING_MAX_DISTANCE;
            //            }
            //            if (lockBitmap.GetPixel(x, y + n) == Color.Black)
            //            {
            //                lockBitmap.SetPixel(x, y, Color.Black);
            //                n = Constants.GROUPING_MAX_DISTANCE;
            //            }
            //        }
            //    }
            //};
            lockBitmap.UnlockBits();

            return lockBitmap;
        }


    }
}
