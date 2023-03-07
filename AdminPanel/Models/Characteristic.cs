using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication2.Models
{
    public partial class Characteristic : IEntityTypeConfiguration<Characteristic>
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int CharacteristicsNameId { get; set; }
        public CharacteristicName CharacteristicsName { get; set; } = null!;
        
        public int CharacteristicsValueId { get; set; }
        public CharacteristicValue CharacteristicsValue { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Characteristic> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.CharacteristicsName)
                    .WithMany(p => p.Characteristics)
                    .HasForeignKey(d => d.CharacteristicsNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.CharacteristicsValue)
                .WithMany(p => p.Characteristics)
                .HasForeignKey(d => d.CharacteristicsValueId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Product)
                .WithMany(p => p.Characteristics)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
