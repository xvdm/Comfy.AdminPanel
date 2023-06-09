using AdminPanel.Models.Entities;

namespace AdminPanel.Models.ViewModels;

public sealed class MainCategoriesBrandsModelsViewModel
{
    public IEnumerable<MainCategory> MainCategories { get; set; } = null!;
    public IEnumerable<Brand> Brands { get; set; } = null!;
    public IEnumerable<Model> Models { get; set; } = null!;
}