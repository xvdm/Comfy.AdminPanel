namespace AdminPanel.Models.ViewModels
{
    public class EditProductViewModel
    {
        public Product Product { get; set; } = null!;
        public IEnumerable<MainCategory> MainCategories { get; set; } = null!;
        public IEnumerable<Subcategory> Subcategories { get; set; } = null!;
    }
}
