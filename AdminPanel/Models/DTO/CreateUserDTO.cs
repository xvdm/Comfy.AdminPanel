﻿using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.DTO
{
    public class CreateUserDTO
    {
        [Required]
        public string Role { get; set; } = null!;

        [Required]
        [MinLength(5)]
        public string UserName { get; set; } = null!;

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}