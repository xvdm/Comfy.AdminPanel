using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models.Entities;

public sealed class CharacteristicGroup : IEntityTypeConfiguration<CharacteristicGroup>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public ICollection<Characteristic> Characteristics { get; set; } = null!;

    public void Configure(EntityTypeBuilder<CharacteristicGroup> builder)
    {
        
    }
}