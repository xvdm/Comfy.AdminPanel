using AdminPanel.Models.Identity;

namespace AdminPanel.Models;

public sealed class WishList
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = null!;
}