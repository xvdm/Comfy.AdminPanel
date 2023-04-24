using AdminPanel.Models.Base;
using AdminPanel.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models;

public class ReviewAnswer : Auditable
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public string Text { get; set; } = null!;
    public int UsefulAnswerCount { get; set; }
    public int NeedlessAnswerCount { get; set; }
    public bool IsActive { get; set; }

    public int ReviewId { get; set; }
    public Review Review { get; set; } = null!;

    public void Configure(EntityTypeBuilder<ReviewAnswer> builder)
    {
        builder.HasOne(d => d.Review)
                .WithMany(p => p.Answers)
                .HasForeignKey(d => d.ReviewId)
                .OnDelete(DeleteBehavior.ClientCascade);
    }
}