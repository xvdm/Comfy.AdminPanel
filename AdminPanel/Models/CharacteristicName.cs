using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public partial class CharacteristicName : IEntityTypeConfiguration<CharacteristicName>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;


        public void Configure(EntityTypeBuilder<CharacteristicName> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
