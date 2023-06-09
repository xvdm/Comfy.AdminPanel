using AdminPanel.Models.Entities;

namespace AdminPanel.Models.ViewModels;

public sealed class ReviewsViewModel
{
    public IEnumerable<Review> Reviews { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}