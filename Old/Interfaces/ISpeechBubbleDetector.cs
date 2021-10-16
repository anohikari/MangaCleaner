using System.Collections.Generic;
using System.Drawing;
using ImageHandler.Klassen;

namespace ImageHandler.Interfaces
{
    interface ISpeechBubbleDetector
    {
        /// <summary>
        /// Returns a list of pixels each of which is part of another speechbubble within the specified image file.
        /// </summary>
        /// <param name="ImageFileLocation"></param>
        /// <returns></returns>
        List<SpeechBubble> DetectSpeechBubbles(string ImageFilePath);
    }
}
