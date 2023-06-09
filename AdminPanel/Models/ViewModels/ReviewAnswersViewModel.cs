using AdminPanel.Models.Entities;

namespace AdminPanel.Models.ViewModels;

public sealed class ReviewAnswersViewModel
{
    public IEnumerable<ReviewAnswer> ReviewAnswers { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}