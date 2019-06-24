using System.Drawing;

namespace ImageHandler
{
    static class Constants
    {
        public static int BUFFERSIZE = 5;
        public static int FLATTENING_THRESHHOLD = 200;
        public static int BUBBLE_MAX_SIZE = 1000000;
        public static int CORNER_CHECK_LIMIT = 100;
        public static int GROUPING_MAX_DISTANCE = 1;
        public static Color MARKED = Color.LightPink;
        public static string SAVEPATH {
            get
            {
                return Properties.Settings.Default.SavePath;
            }
            set
            {
                Properties.Settings.Default.SavePath = value;
                Properties.Settings.Default.Save();

            }
        }
        public static bool MakeTrainingData
        {
            get
            {
                return Properties.Settings.Default.MakeTrainingData;
            }
            set
            {
                Properties.Settings.Default.MakeTrainingData = value;
                Properties.Settings.Default.Save();

            }
        }
    }
}
