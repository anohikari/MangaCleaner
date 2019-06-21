using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace ImageHandler.Klassen
{
    class SpeechBubbleDetector
    {
        public List<SpeechBubble> Detect()
        {
            List<SpeechBubble> DetectedBubbles = new List<SpeechBubble>();
            Mat img = CvInvoke.Imread("myimage.jpg", ImreadModes.AnyColor);
            return DetectedBubbles;
        }
    }
}
