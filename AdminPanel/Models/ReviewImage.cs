using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class ReviewImage : IEntityTypeConfiguration<ReviewImage>
    {
        public int Id { get; set; }
        
        public int ReviewId { get; set; }
        public Review Review { get; set; } = null!;

        public int ImageId { get; set; }
        public Image Image { get; set; } = null!;


        public void Configure(EntityTypeBuilder<ReviewImage> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Image)
                .WithMany(p => p.ReviewsImages)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReviewsImage_Images");

            builder.HasOne(d => d.Review)
                .WithMany(p => p.ReviewImages)
                .HasForeignKey(d => d.ReviewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReviewsImage_Reviews");
        }
    }
}
