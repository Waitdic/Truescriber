﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Truescriber.BLL.Services.Task.Models
{
    public class UploadViewModel
    {
        [Required]
        [Display(Name = "TaskName")]
        public string TaskName { get; set; }

        [Required]
        [Display(Name = "File")]
        public  IFormFile File { get; set; }
    }
}