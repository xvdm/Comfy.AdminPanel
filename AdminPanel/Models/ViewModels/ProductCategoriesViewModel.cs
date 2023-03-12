namespace AdminPanel.Models.ViewModels
{
    public class ProductCategoriesViewModel
    {
        public Product? Product { get; set; }
        public IEnumerable<MainCategory> MainCategories { get; set; } = null!;
        public IEnumerable<Subcategory> Subcategories { get; set; } = null!;
    }
}
