﻿using System.ComponentModel.DataAnnotations;


namespace Truescriber.BLL.IdentityModels
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string UserId { get; set; }
    }
}
