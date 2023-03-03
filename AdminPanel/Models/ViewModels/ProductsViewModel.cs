using WebApplication2.Models;

namespace AdminPanel.Models.ViewModels
{
    public class ProductsViewModel
    {
        public int CategoryId { get; set; }
        public string? Query { get; set; }
        public IEnumerable<Product> Products { get; set; } = null!;
        public IEnumerable<Brand> Brands { get; set; } = null!;
        public IEnumerable<Model> Models { get; set; } = null!;

        // todo: characteristics
    }
}
