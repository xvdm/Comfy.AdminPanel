using AdminPanel.Models.Entities;

namespace AdminPanel.Models.ViewModels;

public sealed class SubcategoryFiltersViewModel
{
    public IEnumerable<SubcategoryFilter> SubcategoryFilters { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}
