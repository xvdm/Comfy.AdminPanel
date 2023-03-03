using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.DTO
{
    public class CreateMainCategoryDTO
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
