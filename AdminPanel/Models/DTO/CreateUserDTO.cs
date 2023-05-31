using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.DTO;

public sealed class CreateUserDTO
{
    [Required]
    public string Role { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string ConfirmPassword { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = null!;
}