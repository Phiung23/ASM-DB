﻿using System.ComponentModel.DataAnnotations;

namespace hospitalapp.Models.AuthModel
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

}
