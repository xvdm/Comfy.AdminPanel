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
        public int Brand { get; set; }

        [Required]
        public int Category { get; set; }

        [Required]
        public int Model { get; set; }

        [Required]
        public string Description { get; set; } = null!;
    }
}
