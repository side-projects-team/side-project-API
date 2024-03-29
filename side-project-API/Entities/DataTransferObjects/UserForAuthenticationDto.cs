﻿using System.ComponentModel.DataAnnotations;

namespace side_project_API.Entities.DataTransferObjects
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        public string? ClientURI { get; set; }
    }
}