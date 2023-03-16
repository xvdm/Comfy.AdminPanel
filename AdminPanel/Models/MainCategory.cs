using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Models
{
    public partial class MainCategory : IEntityTypeConfiguration<MainCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int? ImageId { get; set; }
        public MainCategoryImage? Image { get; set; } = null!;

        public ICollection<Subcategory> Categories { get; set; } = null!;

        public void Configure(EntityTypeBuilder<MainCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsUnicode(false);

            builder.HasOne(x => x.Image);
        }
    }
}
