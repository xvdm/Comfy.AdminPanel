namespace AdminPanel.Models.ViewModels
{
    public class QuestionsViewModel
    {
        public ICollection<Question> Questions { get; set; } = null!;
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
