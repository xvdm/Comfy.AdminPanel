using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Brand : IEntityTypeConfiguration<Brand>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
        }
    }
}
