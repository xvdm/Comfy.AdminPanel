using AdminPanel.Models.Entities;

namespace AdminPanel.Models.ViewModels;

public sealed class CharacteristicValuesViewModel
{
    public IEnumerable<CharacteristicValue> Values { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}