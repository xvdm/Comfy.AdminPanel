using AdminPanel.Models.Base;
using AdminPanel.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models;

public class Review : Auditable, IEntityTypeConfiguration<Review>
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public string Advantages { get; set; } = null!;
    public string Disadvantages { get; set; } = null!;
    public double ProductRating { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public bool IsActive { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public ICollection<ReviewAnswer> Answers { get; set; } = null!;


    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasOne(d => d.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}