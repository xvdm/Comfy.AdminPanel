using AdminPanel.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public partial class Wishlist : IEntityTypeConfiguration<Wishlist>
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(d => d.Products);
        }
    }
}
