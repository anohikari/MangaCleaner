using System.Drawing;

namespace ImageHandler
{
    static class Constants
    {
        public static int BUFFERSIZE = 5;
        public static int FLATTENING_THRESHHOLD = 200;
        public static int BUBBLE_MAX_SIZE = 200000;
        public static int CORNER_CHECK_LIMIT = 100;
        public static int GROUPING_MAX_DISTANCE = 1;
        public static Color MARKED = Color.LightPink;
        public static string SAVEPATH = "default";

        public static void LoadSettings()
        {
            //BUFFERSIZE = Properties.Settings.Default.BufferSize;
            //FLATTENING_THRESHHOLD = Properties.Settings.Default.FlatteningThreshhold;
            //BUBBLE_MAX_SIZE = Properties.Settings.Default.BubbleMaxSize;
            //CORNER_CHECK_LIMIT = Properties.Settings.Default.CornerCheckLimit;
            //GROUPING_MAX_DISTANCE = Properties.Settings.Default.GroupingMaxDistance;
            //SAVEPATH = Properties.Settings.Default.SavePath;
        }

    }
}
