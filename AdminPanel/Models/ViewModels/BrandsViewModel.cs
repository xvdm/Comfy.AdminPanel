namespace AdminPanel.Models.ViewModels;

public sealed class BrandsViewModel
{
    public IEnumerable<Brand> Brands { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}