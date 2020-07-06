using MangaCleaner.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MangaCleaner
{
    /// <summary>
    /// Implements functions for marking and cleaning textbubbles
    /// </summary>
    public unsafe class SpeechBubble
    {
        WriteableBitmap ParentImage;
        WriteableBitmap ResultImage;
        Parser parser;
        Color MarkingColor = Constants.MARKED;

        // Any speechbubble will be instantiated and recognised by a point handed to the constructor
        private Point InitPoint;

        public int Size = 0;

        // Bubbles boundary box for machine learning purposes
        private Int32Rect BoundingBox
        {
            get
            {
                return new Int32Rect(XMin, YMin, XMax - XMin, YMax - YMin);
            }
        }
        private int XMax, YMax = 0;
        private int YMin, XMin = 10000;

        // Required for finding the rest of the bubbles content from the Initpoint
        private Queue<Point> RegionBoundary = new Queue<Point>();
        private Queue<Point> RegionTextPoints = new Queue<Point>();

        public SpeechBubble(ref WriteableBitmap Image,ref WriteableBitmap resImage, Point position)
        {
            InitPoint = new Point((int)position.X,(int)position.Y);
            ParentImage = Image;
            ResultImage = resImage;
            parser = new Parser((byte*)ParentImage.BackBuffer.ToPointer(), ParentImage.BackBufferStride);
        }
        public void CleanBubble()
        {
            ParentImage.Lock();
            unsafe
            {
                parser.Point = InitPoint;

                while (parser.X < ParentImage.Width && parser.Red == 0)
                    parser.X++;
                if (Constants.MarkingColors.Where(x => x.G == parser.Green).Any())
                {
                    ParentImage.Unlock();
                    return;
                }

                ExtendRegion(InitPoint);
                while (RegionBoundary.Count > 0)
                {
                    Size++;
                    if (Size > Constants.BUBBLE_MAX_SIZE)
                    {
                        ParentImage.AddDirtyRect(BoundingBox);
                        ParentImage.Unlock();
                        //ImageHandler.ResultImage.UnlockBits();
                        return;
                    }
                    if (CheckInbounds())
                        ExtendRegion(RegionBoundary.Dequeue());
                    else
                        RegionBoundary.Dequeue();
                }
                DeleteText();
                ParentImage.AddDirtyRect(BoundingBox);
                ParentImage.Unlock();
                //ImageHandler.ResultImage.UnlockBits();
            }

        }
        private bool CheckInbounds()
        {
            return (2 < RegionBoundary.Peek().X
                     && RegionBoundary.Peek().X < ParentImage.PixelWidth - 2
                     && 2 < RegionBoundary.Peek().Y
                     && RegionBoundary.Peek().Y < ParentImage.PixelHeight - 2);
        }

        private void ExtendRegion(Point p)
        {
            if (p.X > XMax)
                XMax = (int)p.X;
            if (p.Y > YMax)
                YMax = (int)p.Y;
            if (p.X < XMin)
                XMin = (int)p.X;
            if (p.Y < YMin)
                YMin = (int)p.Y;


            parser.SetPixel(p, MarkingColor);
            foreach (Point AdjacentPixel in GetAdjacentPixels(p))
            {
                parser.Point = AdjacentPixel;
                if (parser.Blue == 255)
                {
                    RegionBoundary.Enqueue(AdjacentPixel);
                    parser.SetPixel(MarkingColor);
                    //ImageHandler.ResultImage.SetPixel(AdjacentPixel.X, AdjacentPixel.Y, Color.White);
                }
                if (parser.Red == 0)
                {
                    RegionTextPoints.Enqueue(AdjacentPixel);
                }
            }

        }

        private List<Point> GetAdjacentPixels
            (Point p)
        {
            List<Point> Neighbours = new List<Point>
            {
                new Point(p.X + 1, p.Y),
                new Point(p.X - 1, p.Y),
                new Point(p.X, p.Y + 1),
                new Point(p.X, p.Y - 1)
            };
            return Neighbours;
        }

        private void DeleteText()
        {
            ResultImage.Lock();
            Parser ResultParser = new Parser((byte*)ResultImage.BackBuffer.ToPointer(), ResultImage.BackBufferStride);
            Point p = new Point();
            while (RegionTextPoints.Count > 0)
            {
                p = RegionTextPoints.Dequeue();
                if (!CheckCorner(p))
                {
                    for (int i = 0; p.X + i < XMax; i++)
                    {
                        if (parser.GetPixel(p.X + i, p.Y).G == MarkingColor.G)
                        {
                            for (int j = 0; j <= i; j++)
                            {
                                parser.SetPixel((int)(p.X + j),(int) p.Y, Colors.White);
                                ResultParser.SetPixel((int)(p.X + j), (int)p.Y, Colors.White);
                            }
                            i = ParentImage.PixelWidth;
                        }
                        if (p.X + i > ParentImage.PixelWidth - 3)
                            i = ParentImage.PixelWidth;
                    }
                }
            }
            ResultImage.AddDirtyRect(BoundingBox);
            ResultImage.Unlock();

        }
        /// <summary>
        /// Returns false if Pixel can be painted over. Check if the given (black) pixel belongs to the outer bound.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool CheckCorner(Point p)
        {
            bool up, down, right, UpRight, DownRight;
            up = down = right = UpRight = DownRight = false;
            for (int i = 0; i < Constants.CORNER_CHECK_LIMIT; i++)
            {
                if (p.X + i < ParentImage.PixelWidth && parser.GetPixel(p.X + i, p.Y).G == MarkingColor.G)
                    right = true;
                if (p.Y + i < ParentImage.PixelHeight && parser.GetPixel(p.X, p.Y + i).G == MarkingColor.G)
                    up = true;
                if (p.Y - i > 0 && parser.GetPixel(p.X, p.Y - i).G == MarkingColor.G)
                    down = true;
                if (p.Y + i < ParentImage.PixelHeight && p.X + i < ParentImage.PixelWidth)
                {
                    if (parser.GetPixel(p.X + i, p.Y + i).G == MarkingColor.G)
                    {
                        UpRight = true;
                    }
                }
                if (p.Y - i > 0 && p.X + i < ParentImage.PixelWidth)
                {
                    if (parser.GetPixel(p.X + i, p.Y - i).G == MarkingColor.G)
                    {
                        DownRight = true;
                    }
                }
                if (up && down && right && UpRight && DownRight)
                    return false;
            }
            return (!(up && down && right && UpRight && DownRight));
        }
    }
}
