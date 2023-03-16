using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public class SubcategoryImage : IEntityTypeConfiguration<SubcategoryImage>
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;

        public void Configure(EntityTypeBuilder<SubcategoryImage> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
