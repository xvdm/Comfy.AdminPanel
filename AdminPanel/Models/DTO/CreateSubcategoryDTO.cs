using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.DTO
{
    public class CreateSubcategoryDTO
    {
        [Required]
        public int MainCategoryId { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
