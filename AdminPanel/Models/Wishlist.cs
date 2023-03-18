using AdminPanel.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public partial class WishList : IEntityTypeConfiguration<WishList>
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = null!;


        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .HasMany(d => d.Products)
                .WithMany(x => x.WishLists);
        }
    }
}
