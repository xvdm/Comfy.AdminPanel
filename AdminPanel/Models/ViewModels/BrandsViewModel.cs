namespace AdminPanel.Models.ViewModels
{
    public class BrandsViewModel
    {
        public IEnumerable<Brand> Brands { get; set; } = null!;
        public int TotalPages { get; set; }
    }
}
