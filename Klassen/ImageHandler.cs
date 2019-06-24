using System;
using System.Drawing;
using System.Threading.Tasks;

namespace ImageHandler
{
    /// <summary>
    /// Holds all WIP images, and manipulates them
    /// </summary>
    public class ImageHandler
    {

        public LockBitmap CurrentImage;         // preprocessed Image
        public Bitmap QuickReload;
        public LockBitmap ChangeBuffer;         
        public LockBitmap ResultImage;          // Latest WIP

        public byte LowerBound = 0;
        public byte UpperBound = 0;

        private ImageBuffer ImageBuffer;

        public void Init(string FileSource)
        {
            ImageBuffer = new ImageBuffer(this, FileSource);
            Task task = new Task(ImageBuffer.FillBuffer);
            task.Start();

            Bitmap toLock = new Bitmap(FileSource);
            ChangeBuffer = new LockBitmap(new Bitmap(toLock));
            ResultImage = new LockBitmap(new Bitmap(toLock));     // result = new bitmap
            CurrentImage = ImageBuffer.getNextImage();
            QuickReload = new Bitmap(CurrentImage.source);
            Klassen.SpeechBubble.BoundingBoxes.Clear();
            
        }
        public void LoadNextimageFromBuffer()
        {
            if (ImageBuffer.EndReached && ImageBuffer.Imagebuffer.Count == 0)
                return;
            ImageBuffer.Save(ResultImage.source);
            while(ImageBuffer.Imagebuffer.Count == 0) { }
            Bitmap toLock = new Bitmap(ImageBuffer.getNextFile());
            CurrentImage = ImageBuffer.getNextImage();
            QuickReload = new Bitmap(new Bitmap(CurrentImage.source));
            ChangeBuffer = new LockBitmap(new Bitmap(toLock));
            ResultImage = new LockBitmap(new Bitmap(toLock));
            Klassen.SpeechBubble.BoundingBoxes.Clear();
        }
        public void Reload()
        {
            if (ImageBuffer == null)
                return;
            CurrentImage = new LockBitmap(QuickReload);
            ResultImage = new LockBitmap(new Bitmap(ImageBuffer.getCurrentfile()));
        }

        /// <summary>
        /// Stretches the colour spectrum between Lowerbound and Upperbound to the full colourspectrum. (linear)
        /// </summary>
        /// <param name="ResultImage"></param>
        /// <returns></returns>
        public void Level()
        {
            byte r, g, b;
            int avg;
            Color LeveledColor;

            for (int x = 0; x < ResultImage.Width; x++)
            {
                for (int y = 0; y < ResultImage.Height; y++)
                {
                    r = ResultImage.GetPixel(x, y).R;
                    g = ResultImage.GetPixel(x, y).G;
                    b = ResultImage.GetPixel(x, y).B;
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
                    LeveledColor = Color.FromArgb(
                            Convert.ToByte(f * (r - LowerBound)),       
                            Convert.ToByte(f * (g - LowerBound)),
                            Convert.ToByte(f * (b - LowerBound))
                                            );
                    this.ResultImage.SetPixel(x, y, LeveledColor);
                }
            }
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
            int AverageBrightness;
            int CheckWidth = image.Width - 1;
            int CheckHeight = image.Height - 1;
            for (int x = 0; x < CheckWidth; x++)
            {
                for (int y = 0; y < CheckHeight; y++)
                {
                    color = lockBitmap.GetPixel(x, y);
                    AverageBrightness = (int)(color.GetBrightness() *255);
                    if (AverageBrightness < Constants.FLATTENING_THRESHHOLD) 
                        lockBitmap.SetPixel(x, y, Color.Black);
                    else
                        lockBitmap.SetPixel(x, y, Color.White);
                }
            };

            for (int x = 0; x < CheckWidth; x++)
            {
                for (int y = 0; y < CheckHeight; y++)
                {
                    color = lockBitmap.GetPixel(x, y);
                    for (int n = 0; n < Constants.GROUPING_MAX_DISTANCE; n++)
                    {
                        if (lockBitmap.GetPixel(x + n, y) == Color.Black)
                        {
                            lockBitmap.SetPixel(x, y, Color.Black);
                            n = Constants.GROUPING_MAX_DISTANCE;
                        }
                        if (lockBitmap.GetPixel(x, y + n) == Color.Black)
                        {
                            lockBitmap.SetPixel(x, y, Color.Black);
                            n = Constants.GROUPING_MAX_DISTANCE;
                        }
                    }
                }
            };
            lockBitmap.UnlockBits();

            return lockBitmap;
        }
    }
}
