using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public partial class AddressType : IEntityTypeConfiguration<AddressType>
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public ICollection<Address>? Addresses { get; set; }


        public void Configure(EntityTypeBuilder<AddressType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Type).HasMaxLength(50);
        }
    }
}
