using System.Collections.Generic;

namespace Truescriber.Common.Helpers
{
    public class FormatHelper
    {
        private List<string> Formats { get; } = new List<string>()
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

        public List<string> GetFormats()
        {
            return Formats;
        }
        public string SupportedFormatsMessage()
        {
            const string errorMessage = "Supported formats:" +
                                        "\n .flac" +
                                        "\n .raw" +
                                        "\n .wav" +
                                        "\n .mp3" +
                                        "\n .arm-wb" +
                                        "\n .ogg" +
                                        "\n .avi" +
                                        "\n .mp4" +
                                        "\n .mkv" +
                                        "\n .flv";
            return errorMessage;
        }
    }
}
