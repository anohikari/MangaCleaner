﻿using System.Windows.Media;

namespace MangaCleaner
{
    static class Constants
    {
        public static int BUFFERSIZE = 5;
        public static int FLATTENING_THRESHHOLD = 230;
        public static int BUBBLE_MAX_SIZE = 1000000;
        public static int CORNER_CHECK_LIMIT = 100;
        public static int GROUPING_MAX_DISTANCE = 1;
        public static string SAVEPATH = "default";
        public static bool MakeTrainingData = false;
        public static System.Windows.Media.Color MARKED = Colors.Pink;
    }
}
