using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public partial class Review : IEntityTypeConfiguration<Review>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = null!;
        public string Advantages { get; set; } = null!;
        public string Disadvantages { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public double ProductRating { get; set; }
        public int UsefullReviewCount { get; set; }
        public int NeedlesslyReviewCount { get; set; }
        public bool IsActive { get; set; }

        //public ApplicationUser User { get; set; } = null!;
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public ICollection<Answer>? Answers { get; set; }


        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Advantages).HasMaxLength(50);
            builder.Property(e => e.CreateDate).HasColumnType("date");
            builder.Property(e => e.Disadvantages).HasMaxLength(50);

            builder.HasOne(d => d.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
