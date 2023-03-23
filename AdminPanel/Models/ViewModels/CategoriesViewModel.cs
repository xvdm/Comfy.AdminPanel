namespace AdminPanel.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public IEnumerable<MainCategory> MainCategories { get; set; } = null!;
        public IEnumerable<Subcategory> Subcategories { get; set; } = null!;
        public IEnumerable<Brand> Brands { get; set; } = null!;
        public IEnumerable<Model> Models { get; set; } = null!;
    }
}
