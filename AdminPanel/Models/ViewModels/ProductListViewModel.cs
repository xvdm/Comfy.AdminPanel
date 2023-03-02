using WebApplication2.Models;

namespace AdminPanel.Models.ViewModels
{
    public class ProductListViewModel
    {
        public string? Query { get; set; }
        public IEnumerable<Product> Products { get; set; } = null!;
        public IEnumerable<Brand> Brands { get; set; } = null!;
        public IEnumerable<Model> Models { get; set; } = null!;
        public IEnumerable<Category> Categories { get; set; } = null!;
        // todo: characteristics
    }
}
