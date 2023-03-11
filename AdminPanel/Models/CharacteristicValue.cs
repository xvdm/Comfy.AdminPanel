using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public partial class CharacteristicValue : IEntityTypeConfiguration<CharacteristicValue>
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;


        public void Configure(EntityTypeBuilder<CharacteristicValue> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
