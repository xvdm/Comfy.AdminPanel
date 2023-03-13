using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.DTO
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        public string Brand { get; set; } = null!;

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        public string Model { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;
    }
}
