using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Truescriber.BLL.UploadModel
{
    public class TaskModel
    {
        [Required]
        [Display(Name = "TaskName")]
        public string TaskName { get; set; }

        [Required]
        [Display(Name = "File")]
        public  IFormFile File { get; set; }
        public string UserId { get; set; }
    }
}
