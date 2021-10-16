using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MangaCleaner.Classes
{
    static class FreeOCRWrapper
    {
        public static async Task<PointCollection> OCR(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;
            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(1, 1, 1);


            var form = new MultipartFormDataContent();
            form.Add(new StringContent("a60b07a4bd88957"), "apikey"); //Added api key in form data
            form.Add(new StringContent("jpn"), "language");

            form.Add(new StringContent("1"), "ocrengine");
            form.Add(new StringContent("true"), "scale");
            form.Add(new StringContent("false"), "istable");
            form.Add(new StringContent("true"), "isOverlayRequired");

            byte[] imageData = File.ReadAllBytes(filePath);
            form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg");

            var response = await httpClient.PostAsync("https://api.ocr.space/Parse/Image", form);
            var strContent = await response.Content.ReadAsStringAsync();
            var ocrResult = JsonConvert.DeserializeObject<Root>(strContent);

            var result = new PointCollection();
            foreach (var word in ocrResult.ParsedResults.
                Select(x => x.TextOverlay).
                SelectMany(x => x.Lines).
                SelectMany(x => x.Words))
            {
                result.Add(new System.Windows.Point(word.Left, word.Top));
            }
            return result;
        }
    }

    public class Word
    {
        public string WordText { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
    }

    public class Line
    {
        public string LineText { get; set; }
        public List<Word> Words { get; set; }
        public double MaxHeight { get; set; }
        public double MinTop { get; set; }
    }

    public class TextOverlay
    {
        public List<Line> Lines { get; set; }
        public bool HasOverlay { get; set; }
        public string Message { get; set; }
    }

    public class ParsedResult
    {
        public TextOverlay TextOverlay { get; set; }
        public string TextOrientation { get; set; }
        public int FileParseExitCode { get; set; }
        public string ParsedText { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }

    public class Root
    {
        public List<ParsedResult> ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string ProcessingTimeInMilliseconds { get; set; }
        public string SearchablePDFURL { get; set; }
    }
}
