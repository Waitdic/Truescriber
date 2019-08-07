using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Truescriber.BLL.UploadModel
{
    public class UploadViewModel
    {
        [Required]
        [Display(Name = "TaskName")]
        public string TaskName { get; set; }

        [Required]
        [Display(Name = "File")]
        public  IFormFile File { get; set; }

        public string UserId { get; set; }

        /*public bool FormatValid(string format)
        {
            var audioFormats = new List<string>()
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
            var form = audioFormats.Find((x) => x == format);

            return !string.IsNullOrWhiteSpace(form);
        }*/

        /*public string GetErrorMessage()
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
        }/*/

        public void GetUser()
        {

        }
    }
}
