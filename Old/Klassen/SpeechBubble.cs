﻿using System.Collections.Generic;
using System.Drawing;

namespace ImageHandler.Klassen
{
    /// <summary>
    /// Implements functions for marking and cleaning textbubbles
    /// </summary>
    public class SpeechBubble
    {
        // References to the images currently used, instantiated at program start by UcImagehandler
        public static ImageHandler ImageHandler;
        public static LockBitmap CurrentImage;

        public static HashSet<Rectangle> BoundingBoxes = new HashSet<Rectangle>();

        // Any speechbubble will be instantiated and recognised by a point handed to the constructor
        private Point InitPoint;

        // Bubbles boundary box for machine learning purposes
        private int XMin = 10000;
        private int YMin = 10000;
        private int XMax, YMax = 0;
        private int Size = 0;

        // Required for finding the rest of the bubbles content from the Initpoint
        private Queue<Point> RegionBoundary = new Queue<Point>();
        private Queue<Point> RegionTextPoints = new Queue<Point>();

        public SpeechBubble(int XInit, int YInit)
        {
            InitPoint = new Point(XInit, YInit);
            CurrentImage = ImageHandler.CurrentImage;
        }
        public void CleanBubble()
        {
            CurrentImage.LockBits();
            ImageHandler.ResultImage.LockBits();
            Size++;

            while (InitPoint.X < CurrentImage.Width && CurrentImage.GetPixel(InitPoint.X, InitPoint.Y).G == Color.Black.G)
                InitPoint.X++;
            if (CurrentImage.GetPixel(InitPoint.X, InitPoint.Y).G == Constants.MARKED.G)
            {
                CurrentImage.UnlockBits();
                ImageHandler.ResultImage.UnlockBits();
                return;
            }

            ExtendRegion(InitPoint);
            while (RegionBoundary.Count > 0)
            {
                Size++;
                if (Size > Constants.BUBBLE_MAX_SIZE)
                {
                    CurrentImage.UnlockBits();
                    ImageHandler.ResultImage.UnlockBits();
                    return;
                }
                if (CheckInbounds())
                    ExtendRegion(RegionBoundary.Dequeue());
                else
                    RegionBoundary.Dequeue();
            }
            DeleteText();
            BoundingBoxes.Add(new Rectangle(XMin, YMin, XMax - XMin, YMax - YMin));
            CurrentImage.UnlockBits();
            ImageHandler.ResultImage.UnlockBits();

        }
        private bool CheckInbounds()
        {
            return (2 < RegionBoundary.Peek().X
                     && RegionBoundary.Peek().X < CurrentImage.Width - 2
                     && 2 < RegionBoundary.Peek().Y
                     && RegionBoundary.Peek().Y < CurrentImage.Height - 2);
        }

        private void ExtendRegion(Point p)
        {
            if (p.X > XMax)
                XMax = p.X;
            if (p.Y > YMax)
                YMax = p.Y;
            if (p.X < XMin)
                XMin = p.X;
            if (p.Y < YMin)
                YMin = p.Y;


            CurrentImage.SetPixel(p.X, p.Y, Constants.MARKED);
            foreach (Point AdjacentPixel in GetAdjacentPixels(p))
            {
                if (CurrentImage.GetPixel(AdjacentPixel.X, AdjacentPixel.Y).G == 255)
                {
                    RegionBoundary.Enqueue(AdjacentPixel);
                    CurrentImage.SetPixel(AdjacentPixel.X, AdjacentPixel.Y, Constants.MARKED);
                    ImageHandler.ResultImage.SetPixel(AdjacentPixel.X, AdjacentPixel.Y, Color.White);
                }
                if (CurrentImage.GetPixel(AdjacentPixel.X, AdjacentPixel.Y).G == 0)
                {
                    RegionTextPoints.Enqueue(AdjacentPixel);
                }
            }

        }
        List<Point> GetAdjacentPixels(Point p)
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
            Point p = new Point();
            while (RegionTextPoints.Count > 0)
            {
                p = RegionTextPoints.Dequeue();
                if (!CheckCorner(p))
                {
                    for (int i = 0; p.X + i < XMax; i++)
                    {
                        if (CurrentImage.GetPixel(p.X + i, p.Y).G == Constants.MARKED.G)
                        {
                            for (int j = 0; j <= i; j++)
                            {
                                ImageHandler.ResultImage.SetPixel(p.X + j, p.Y, Color.White);
                            }
                            i = CurrentImage.Width;
                        }
                        if (p.X + i > CurrentImage.Width - 3)
                            i = CurrentImage.Width;
                    }
                }
            }

        }
        /// <summary>
        /// Returns false if Pixel can be painted over. Check if the given (black) pixel belongs to the outer bound.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool CheckCorner(Point p)
        {
            bool up, down, left, right, UpRight, DownRight;
            up = down = left = right = UpRight = DownRight = false;
            for (int i = 0; i < Constants.CORNER_CHECK_LIMIT; i++)
            {
                if (p.X + i < CurrentImage.Width && CurrentImage.GetPixel(p.X + i, p.Y).G == Constants.MARKED.G)
                    right = true;
                if (p.Y + i < CurrentImage.Height && CurrentImage.GetPixel(p.X, p.Y + i).G == Constants.MARKED.G)
                    up = true;
                if (p.Y - i > 0 && CurrentImage.GetPixel(p.X, p.Y - i).G == Constants.MARKED.G)
                    down = true;
                if (p.Y + i < CurrentImage.Height && p.X + i < CurrentImage.Width)
                    if (CurrentImage.GetPixel(p.X + i, p.Y + i).G == Constants.MARKED.G)
                        UpRight = true;
                if (p.Y - i > 0 && p.X + i < CurrentImage.Width)
                    if (CurrentImage.GetPixel(p.X + i, p.Y - i).G == Constants.MARKED.G)
                        DownRight = true;


            }
            return (!(up && down && right && UpRight && DownRight));
        }

    }
}
