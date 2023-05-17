namespace AdminPanel.Models.ViewModels;

public sealed class ModelsViewModel
{
    public IEnumerable<Model> Models { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}