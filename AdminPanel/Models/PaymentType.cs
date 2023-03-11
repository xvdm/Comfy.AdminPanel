using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public partial class PaymentType : IEntityTypeConfiguration<PaymentType>
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public ICollection<Order>? Orders { get; set; }

        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
