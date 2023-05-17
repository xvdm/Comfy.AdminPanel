namespace AdminPanel.Models.ViewModels;

public sealed class ProductsViewModel
{
    public IEnumerable<Product> Products { get; set; } = null!;
}