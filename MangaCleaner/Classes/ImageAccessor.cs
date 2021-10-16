using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace MangaCleaner.Classes
{
    public unsafe class ImageAccessor
    {
        public unsafe ImageAccessor(byte* array, int stride)
        {
            Array = array;
            Stride = stride;
        }

        private byte* Array;

        private int Stride;
        private int ArrayPosition
        {
            get => (Y * Stride) + (X * 4);
        }

        public Byte Red
        {
            get => Array[ArrayPosition];
            set => Array[ArrayPosition] = value;
        }

        public Byte Green
        {
            get => Array[ArrayPosition + 1];
            set => Array[ArrayPosition + 1] = value;
        }

        public Byte Blue
        {
            get => Array[ArrayPosition + 2];
            set => Array[ArrayPosition + 2] = value;
        }

        public int Brightness
        {
            get => ((int)Red + (int)Green + (int)Blue) / 3;
        }

        public bool IsWhite
        {
            get => Blue == 255;
        }

        public bool IsBlack
        {
            get => Red == 0;
        }

        public int X, Y;

        public Point Point
        {
            get => new Point(X, Y);
            set
            {
                X = (int)value.X;
                Y = (int)value.Y;
            }
        }

        public void SetPixel(Color c)
        {
            Red = c.R;
            Green = c.G;
            Blue = c.B;
        }
        public void SetPixel(int x, int y, Color c)
        {
            X = x;
            Y = y;
            SetPixel(c);
        }
        public void SetPixel(Point p, Color c)
        {
            SetPixel((int)p.X, (int)p.Y, c);
        }

        public Color GetPixel()
        {
            return Color.FromArgb(255, Red, Green, Blue);
        }

        public Color GetPixel(Point p)
        {
            Point = p;
            return GetPixel();
        }

        public Color GetPixel(double x, double y)
        {
            return GetPixel(new Point(x, y));
        }
    }
}
