using AdminPanel.Models.DTO;

namespace AdminPanel.Models.ViewModels;

public sealed class UsersViewModel
{
    public IEnumerable<UserDTO> Users { get; set; } = null!;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}