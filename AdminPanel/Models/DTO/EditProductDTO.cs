using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.DTO
{
    public class EditProductDTO
    {
        [Required]
        public int Id { get; set; }

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

        [Required]
        public int DiscountAmount { get; set; }
    }
}
