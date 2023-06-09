using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.ViewModels;

public sealed class LoginViewModel
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [ValidateNever]
    public string ReturnUrl { get; set; } = null!;
}