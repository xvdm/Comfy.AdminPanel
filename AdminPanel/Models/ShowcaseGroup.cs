using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public class ShowcaseGroup : IEntityTypeConfiguration<ShowcaseGroup>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string QueryString { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = null!;

        public void Configure(EntityTypeBuilder<ShowcaseGroup> builder)
        {
            builder.HasMany(x => x.Products)
                .WithMany(x => x.ShowcaseGroups);
        }
    }
}
