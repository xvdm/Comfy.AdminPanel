using AdminPanel.Models.Entities;

namespace AdminPanel.Models.ViewModels;

public sealed class QuestionAnswersViewModel
{
    public IEnumerable<QuestionAnswer> QuestionAnswers { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}