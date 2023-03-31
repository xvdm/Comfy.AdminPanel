namespace AdminPanel.Models.ViewModels
{
    public class ReviewsViewModel
    {
        public ICollection<Review> Reviews { get; set; } = null!;
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
