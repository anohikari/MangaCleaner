using System.Collections.Generic;
using System.Windows.Media;

namespace MangaCleaner
{
    static class Constants
    {
        public const int BUFFERSIZE = 5;
        public const int FLATTENING_THRESHHOLD = 230;
        public const int BUBBLE_MAX_SIZE = 1000000;
        public const int CORNER_CHECK_LIMIT = 200;
        public const int GROUPING_MAX_DISTANCE = 5;
        public const string SAVEPATH = "default";
        public const bool MakeTrainingData = false;
        public static Color MARKED
        {
            get
            {
                if (!ColorEnumerator.MoveNext())
                {
                    ColorEnumerator.Reset();
                    ColorEnumerator.MoveNext();
                }
                return ColorEnumerator.Current; 
            }
        }
        public readonly static List<Color> MarkingColors = new List<Color>() { Colors.Pink, Colors.CadetBlue, Colors.PaleVioletRed };
        private readonly static IEnumerator<Color> ColorEnumerator = MarkingColors.GetEnumerator();
    }
}
