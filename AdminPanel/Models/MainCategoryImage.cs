using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Models
{
    public partial class MainCategoryImage : IEntityTypeConfiguration<MainCategoryImage>
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;

        public void Configure(EntityTypeBuilder<MainCategoryImage> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
