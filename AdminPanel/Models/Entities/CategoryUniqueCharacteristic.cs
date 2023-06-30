using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models.Entities;

public sealed class CategoryUniqueCharacteristic : IEntityTypeConfiguration<CategoryUniqueCharacteristic>
{
    public int Id { get; set; }

    public int SubcategoryId { get; set; }
    public Subcategory Subcategory { get; set; } = null!;

    public int CharacteristicNameId { get; set; }
    public CharacteristicName CharacteristicName { get; set; } = null!;

    public int CharacteristicValueId { get; set; }
    public CharacteristicValue CharacteristicValue { get; set; } = null!;


    public void Configure(EntityTypeBuilder<CategoryUniqueCharacteristic> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.SubcategoryId);
        builder.HasIndex(x => x.CharacteristicNameId);
        builder.HasIndex(x => x.CharacteristicValueId);
    }
}