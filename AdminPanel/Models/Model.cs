using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public partial class Model : IEntityTypeConfiguration<Model>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                    .IsUnicode(false);
        }
    }
}
