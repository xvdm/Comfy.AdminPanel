using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public class Brand : IEntityTypeConfiguration<Brand>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        private ICollection<Subcategory> Subcategories { get; set; } = null!;

        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasMany(x => x.Subcategories)
                .WithMany(x => x.UniqueBrands);
        }
    }
}
