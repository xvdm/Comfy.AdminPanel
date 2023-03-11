namespace AdminPanel.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public IEnumerable<MainCategory> MainCategories { get; set; } = null!;
        public IEnumerable<Subcategory> Subcategories { get; set; } = null!;
    }
}
