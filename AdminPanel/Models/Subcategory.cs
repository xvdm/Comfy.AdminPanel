using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public class Subcategory : IEntityTypeConfiguration<Subcategory>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; } = null!;

        public int? ImageId { get; set; }
        public SubcategoryImage? Image { get; set; } = null!;

        public ISet<Characteristic> UniqueCharacteristics { get; set; } = null!;
        public ISet<Brand> UniqueBrands { get; set; } = null!;

        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.HasOne(x => x.MainCategory)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.MainCategoryId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
