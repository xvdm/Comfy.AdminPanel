using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class CategoryModel : IEntityTypeConfiguration<CategoryModel>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Product>? Products { get; set; }


        public void Configure(EntityTypeBuilder<CategoryModel> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsUnicode(false);
        }
    }
}
