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
        private WriteableBitmap InternalImage;
        private WriteableBitmap ResultImage;
        private ImageAccessor ImageAccessor;
        private Color MarkingColor = Constants.MARKED;

        // Any speechbubble will be instantiated and recognised by a point handed to the constructor
        private Point InitPoint;

        private int Size = 0;

        // Bubbles boundary box for machine learning purposes
        private Int32Rect BoundingBox
        {
            get => new Int32Rect(XMin, YMin, XMax - XMin, YMax - YMin);
        }
        private int XMax, YMax = 0;
        private int YMin, XMin = 10000;

        // Required for finding the rest of the bubbles content from the Initpoint
        private Queue<Point> RegionBoundary = new Queue<Point>();
        private Queue<Point> RegionTextPoints = new Queue<Point>();

        public SpeechBubble(WriteableBitmap internalImage, WriteableBitmap resultImage, Point position)
        {
            InitPoint = position;
            InternalImage = internalImage;
            ResultImage = resultImage;
            ImageAccessor = new ImageAccessor((byte*)InternalImage.BackBuffer.ToPointer(), InternalImage.BackBufferStride);
        }
        public void CleanBubble()
        {
            InternalImage.Lock();
            unsafe
            {
                ImageAccessor.Point = InitPoint;

                while (ImageAccessor.X < InternalImage.Width && ImageAccessor.Red == 0)
                    ImageAccessor.X++;
                if (Constants.MarkingColors.Where(x => x.G == ImageAccessor.Green).Any())
                {
                    InternalImage.Unlock();
                    return;
                }

                ExtendRegion(InitPoint);
                while (RegionBoundary.Count > 0)
                {
                    Size++;
                    if (Size > Constants.BUBBLE_MAX_SIZE)
                    {
                        InternalImage.AddDirtyRect(BoundingBox);
                        InternalImage.Unlock();
                        return;
                    }
                    if (CheckInbounds())
                        ExtendRegion(RegionBoundary.Dequeue());
                    else
                        RegionBoundary.Dequeue();
                }
                DeleteText();
                InternalImage.AddDirtyRect(BoundingBox);
                InternalImage.Unlock();
            }

        }
        private bool CheckInbounds()
        {
            return (2 < RegionBoundary.Peek().X
                     && RegionBoundary.Peek().X < InternalImage.PixelWidth - 2
                     && 2 < RegionBoundary.Peek().Y
                     && RegionBoundary.Peek().Y < InternalImage.PixelHeight - 2);
        }

        private void ExtendRegion(Point p)
        {
            ExtendBoundingBox(p);

            ImageAccessor.SetPixel(p, MarkingColor);
            foreach (Point AdjacentPixel in GetAdjacentPixels(p))
            {
                ImageAccessor.Point = AdjacentPixel;
                if (ImageAccessor.IsWhite)
                {
                    RegionBoundary.Enqueue(AdjacentPixel);
                    ImageAccessor.SetPixel(MarkingColor);
                }
                if (ImageAccessor.IsBlack)
                {
                    RegionTextPoints.Enqueue(AdjacentPixel);
                }
            }
        }

        private void ExtendBoundingBox(Point p)
        {
            if (p.X > XMax)
                XMax = (int)p.X;
            if (p.Y > YMax)
                YMax = (int)p.Y;
            if (p.X < XMin)
                XMin = (int)p.X;
            if (p.Y < YMin)
                YMin = (int)p.Y;
        }

        private List<Point> GetAdjacentPixels(Point p)
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
            ImageAccessor ResultParser = new ImageAccessor((byte*)ResultImage.BackBuffer.ToPointer(), ResultImage.BackBufferStride);
            Point p = new Point();
            while (RegionTextPoints.Count > 0)
            {
                p = RegionTextPoints.Dequeue();
                if (!CheckCorner(p))
                {
                    for (int i = 0; p.X + i < XMax; i++)
                    {
                        if (ImageAccessor.GetPixel(p.X + i, p.Y).G == MarkingColor.G)
                        {
                            for (int j = 0; j <= i; j++)
                            {
                                if(ImageAccessor.GetPixel((int)(p.X + j), (int)p.Y) == Colors.Black)
                                {
                                    ImageAccessor.SetPixel((int)(p.X + j),(int) p.Y, Colors.White);
                                    ResultParser.SetPixel((int)(p.X + j), (int)p.Y, Colors.White);
                                }
                            }
                            i = InternalImage.PixelWidth;
                        }
                        if (p.X + i > InternalImage.PixelWidth - 3)
                            i = InternalImage.PixelWidth;
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
            if(ImageAccessor.GetPixel(p) != Colors.Black) return true;
            bool up, down, right, UpRight, DownRight, UpLeft, DownLeft;
            up = down = right = UpRight = DownRight = UpLeft = DownLeft = false;
            for (int i = 0; i < Constants.CORNER_CHECK_LIMIT; i++)
            {
                if (p.X + i < InternalImage.PixelWidth && ImageAccessor.GetPixel(p.X + i, p.Y).G == MarkingColor.G)
                    right = true;
                if (p.Y + i < InternalImage.PixelHeight && ImageAccessor.GetPixel(p.X, p.Y + i).G == MarkingColor.G)
                    up = true;
                if (p.Y - i > 0 && ImageAccessor.GetPixel(p.X, p.Y - i).G == MarkingColor.G)
                    down = true;
                if (p.Y + i < InternalImage.PixelHeight && p.X + i < InternalImage.PixelWidth)
                {
                    if (ImageAccessor.GetPixel(p.X + i, p.Y + i).G == MarkingColor.G)
                    {
                        UpRight = true;
                    }
                }
                if (p.Y - i > 0 && p.X + i < InternalImage.PixelWidth)
                {
                    if (ImageAccessor.GetPixel(p.X + i, p.Y - i).G == MarkingColor.G)
                    {
                        DownRight = true;
                    }
                }
                if (p.Y - i > 0 && p.X - i > 0)
                {
                    if (ImageAccessor.GetPixel(p.X - i, p.Y - i).G == MarkingColor.G)
                    {
                        UpLeft = true;
                    }
                }
                if (p.Y + i < InternalImage.PixelHeight && p.X - i > 0)
                {
                    if (ImageAccessor.GetPixel(p.X - i, p.Y + i).G == MarkingColor.G)
                    {
                        DownLeft = true;
                    }
                }
                if (up && down && right && UpRight && DownRight && UpLeft && DownLeft)
                    return false;
            }
            return (!(up && down && right && UpRight && DownRight && UpLeft && DownLeft));
        }

    }
}
