using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.DTO;

public sealed class EditProductDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public int Brand { get; set; }

    [Required]
    public int Category { get; set; }

    [Required]
    public int Model { get; set; }

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public int DiscountAmount { get; set; }
}