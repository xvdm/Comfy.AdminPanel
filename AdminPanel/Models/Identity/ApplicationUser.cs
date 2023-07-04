using AdminPanel.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Models.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = null!;
}