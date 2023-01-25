using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace AdminPanel.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string ReturnUrl { get; set; } = null!;
    }
}
