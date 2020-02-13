using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImageHandler.Klassen
{
    class SpeechBubbleDetector : Interfaces.ISpeechBubbleDetector
    {
        public List<SpeechBubble> DetectSpeechBubbles(string ImageFile)
        {
            List<SpeechBubble> DetectedBubbles = new List<SpeechBubble>();
            //Mat img = CvInvoke.Imread("myimage.jpg", ImreadModes.AnyColor);
            return DetectedBubbles;
        }
    }
}
