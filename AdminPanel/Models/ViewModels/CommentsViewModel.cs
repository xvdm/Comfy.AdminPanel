namespace AdminPanel.Models.ViewModels
{
    public class CommentsViewModel
    {
        public ICollection<Question> Questions { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = null!;
    }
}
