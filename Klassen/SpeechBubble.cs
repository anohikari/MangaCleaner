using System.Collections.Generic;
using System.Drawing;

namespace ImageHandler.Klassen
{
    public class SpeechBubble
    {
        static Color MARKED = Color.Pink;
        public static MyImages Status;
        public static LockBitmap Images;            //currentImages
        static List<SpeechBubble> Bubbles;

        public int XMin, YMin = 10000;
        int XMax, YMax = 0;
        int size = 0;
        Queue<Point> RegionBoundary = new Queue<Point>();
        Queue<Point> Text = new Queue<Point>();

        public void MarkBubble(Point p)
        {
            Images.LockBits();
            Status.result.LockBits();
            size++;
            MarkPoint(p);
            while (RegionBoundary.Count > 0)
            {
                size++;
                if (size > Constants.BUBBLE_MAX_SIZE)
                    return;
                if (2 < RegionBoundary.Peek().X && RegionBoundary.Peek().X < Images.Width - 2 && 2 < RegionBoundary.Peek().Y && RegionBoundary.Peek().Y < Images.Height - 2)     //check inbounds
                    MarkPoint(RegionBoundary.Dequeue());
            }
            DeleteText();
            Images.UnlockBits();
            Status.result.UnlockBits();
        }
        private void MarkPoint(Point p)
        {
            if (p.X > XMax)
                XMax = p.X;
            if (p.Y > YMax)
                YMax = p.Y;
            if (p.X < XMin)
                XMin = p.X;
            if (p.Y < YMin)
                YMin = p.Y;
            

            Images.SetPixel(p.X, p.Y, MARKED);
            if (Images.GetPixel(p.X + 1, p.Y).G == 255)
            {
                RegionBoundary.Enqueue(new Point(p.X + 1, p.Y));
                Images.SetPixel(p.X + 1, p.Y, MARKED);
                Status.result.SetPixel(p.X + 1, p.Y, Color.White);
            }
            if (Images.GetPixel(p.X - 1, p.Y).G == 255)
            {
                RegionBoundary.Enqueue(new Point(p.X - 1, p.Y));
                Images.SetPixel(p.X - 1, p.Y, MARKED);
                Status.result.SetPixel(p.X - 1, p.Y, Color.White);
            }
            if (Images.GetPixel(p.X, p.Y + 1).G == 255)
            {
                RegionBoundary.Enqueue(new Point(p.X, p.Y + 1));
                Images.SetPixel(p.X, p.Y + 1, MARKED);
                Status.result.SetPixel(p.X, p.Y + 1, Color.White);
            }
            if (Images.GetPixel(p.X, p.Y - 1).G == 255)
            {
                RegionBoundary.Enqueue(new Point(p.X, p.Y - 1));
                Images.SetPixel(p.X, p.Y - 1, MARKED);
                Status.result.SetPixel(p.X, p.Y - 1, Color.White);
            }

            if (Images.GetPixel(p.X + 1, p.Y).G == 0)
            {
                Text.Enqueue(new Point(p.X + 1, p.Y));
            }
            if (Images.GetPixel(p.X - 1, p.Y).G == 0)
            {
                Text.Enqueue(new Point(p.X - 1, p.Y));
            }
            if (Images.GetPixel(p.X, p.Y + 1).G == 0)
            {
                Text.Enqueue(new Point(p.X, p.Y + 1));
            }
            if (Images.GetPixel(p.X, p.Y - 1).G == 0)
            {
                Text.Enqueue(new Point(p.X, p.Y - 1));
            }


        }
        public SpeechBubble(int XInit, int YInit)
        {
            Point p = new Point(XInit, YInit);
            MarkBubble(p);

        }
        public static void addBubble(int XInit, int YInit)
        {
            SpeechBubble sb = new SpeechBubble(XInit, YInit);
        }

        private void DeleteText()
        {
            Point p = new Point();
            while(Text.Count > 0)
            {
                p = Text.Dequeue();
                if (!CheckCorner(p))
                {
                    for (int i = 0; p.X + i < XMax; i++)
                    {
                        if (Images.GetPixel(p.X + i, p.Y).G == MARKED.G)
                        {
                            for (int j = 0; j <= i; j++)
                            {
                                Status.result.SetPixel(p.X + j, p.Y, Color.White);
                            }
                            i = Images.Width;
                        }
                        if (p.X + i > Images.Width - 3)
                            i = Images.Width;
                    }
                }
            }
            
        }
        /// <summary>
        /// Check if the given (black) pixel belongs to the outer bound
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool CheckCorner(Point p)
        {
            bool up, down, left, right,UpRight,DownRight;
            up = down = left = right = UpRight = DownRight = false;
            for (int i = 0; i < Constants.CORNER_CHECK_LIMIT; i++)
            {
                if (p.X + i < Images.Width && Images.GetPixel(p.X + i, p.Y).G == MARKED.G)
                    right = true;
                //if (p.X - i > 0 && Images.GetPixel(p.X - i, p.Y).G == MARKED.G)
                //    left = true;
                if (p.Y + i < Images.Height && Images.GetPixel(p.X, p.Y + i).G == MARKED.G)
                    up = true;
                if (p.Y - i > 0 && Images.GetPixel(p.X, p.Y - i).G == MARKED.G)
                    down = true;
                if (p.Y + i < Images.Height && Images.GetPixel(p.X + i, p.Y + i).G == MARKED.G)
                    UpRight = true;
                if (p.Y - i > 0 && Images.GetPixel(p.X + i, p.Y - i).G == MARKED.G)
                    DownRight = true;


            }
            return (!(up && down && right && UpRight && DownRight));
        }
        
    }
}
