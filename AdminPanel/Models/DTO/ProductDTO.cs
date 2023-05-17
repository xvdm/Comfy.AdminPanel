using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.DTO;

public sealed class ProductDTO
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public int BrandId { get; set; }

    [Required]
    public int SubcategoryId { get; set; }

    [Required]
    public int ModelId { get; set; }

    [Required]
    public string Description { get; set; } = null!;
}