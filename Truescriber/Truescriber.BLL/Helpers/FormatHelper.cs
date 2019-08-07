using System.Collections.Generic;

namespace Truescriber.BLL.Helpers
{
    public class FormatHelper
    {
        private List<string> Format { get; } = new List<string>()
        {
            "audio/flac",
            "audio/raw",
            "audio/wav",
            "audio/mp3",
            "audio/arm-wb",
            "audio/ogg",
            "video/avi",
            "video/mp4",
            "video/mkv",
            "video/flv"
        };

        public List<string> GetFormat()
        {
            return Format;
        }
    }
}
