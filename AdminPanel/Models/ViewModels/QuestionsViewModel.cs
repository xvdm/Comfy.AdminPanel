namespace AdminPanel.Models.ViewModels;

public sealed class QuestionsViewModel
{
    public IEnumerable<Question> Questions { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}