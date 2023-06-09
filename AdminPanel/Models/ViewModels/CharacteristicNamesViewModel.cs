using AdminPanel.Models.Entities;

namespace AdminPanel.Models.ViewModels;

public sealed class CharacteristicNamesViewModel
{
    public IEnumerable<CharacteristicName> Names { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}