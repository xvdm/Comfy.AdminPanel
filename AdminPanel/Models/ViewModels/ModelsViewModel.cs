namespace AdminPanel.Models.ViewModels
{
    public class ModelsViewModel
    {
        public IEnumerable<Model> Models { get; set; } = null!;
        public int TotalPages { get; set; }
    }
}
