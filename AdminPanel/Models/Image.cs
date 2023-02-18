using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Image : IEntityTypeConfiguration<Image>
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;

        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<ReviewImage>? ReviewsImages { get; set; }


        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
