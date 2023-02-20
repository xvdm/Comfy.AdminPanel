using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Category : IEntityTypeConfiguration<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Product>? Products { get; set; }


        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsUnicode(false);
        }
    }
}
