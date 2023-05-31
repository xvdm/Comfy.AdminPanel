using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.DTO;

public sealed class UserDTO
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Position { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = null!;
}