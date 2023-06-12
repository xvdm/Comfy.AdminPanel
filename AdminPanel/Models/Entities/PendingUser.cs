using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models.Entities;

public sealed class PendingUser : IEntityTypeConfiguration<PendingUser>
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string ConfirmationCode { get; set; } = null!;
    public int AccessFailedCount { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public void Configure(EntityTypeBuilder<PendingUser> builder)
    {
        builder.HasIndex(x => x.Email);
    }
}