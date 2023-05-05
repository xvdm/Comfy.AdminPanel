namespace AdminPanel.Models.ViewModels;

public class ReviewAnswersViewModel
{
    public IEnumerable<ReviewAnswer> ReviewAnswers { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}