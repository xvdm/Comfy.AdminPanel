using AdminPanel.Models.Entities;

namespace AdminPanel.Models.ViewModels;

public sealed class CategoriesViewModel
{
    public IEnumerable<MainCategory> MainCategories { get; set; } = null!;
    public IEnumerable<Subcategory> Subcategories { get; set; } = null!;
}