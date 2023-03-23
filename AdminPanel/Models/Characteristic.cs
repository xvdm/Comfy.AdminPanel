using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public class Characteristic : IEntityTypeConfiguration<Characteristic>
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!; // возможно надо закомментить вместе с конфигом

        public int CharacteristicsNameId { get; set; }
        public CharacteristicName CharacteristicsName { get; set; } = null!;
        
        public int CharacteristicsValueId { get; set; }
        public CharacteristicValue CharacteristicsValue { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Characteristic> builder)
        {
            builder.HasOne(d => d.Product)
                .WithMany(p => p.Characteristics)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
