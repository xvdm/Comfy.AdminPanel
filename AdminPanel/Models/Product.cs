using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace WebApplication2.Models
{
    public partial class Product : IEntityTypeConfiguration<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public int Price { get; set; }
        public int DiscountAmmount { get; set; }
        public int Amount { get; set; }
        public int Code { get; set; }
        public double Rating { get; set; } 
        public bool IsActive { get; set; }
        public string? Url { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int ModelId { get; set; }
        public Model Model { get; set; } = null!;

        public ICollection<Characteristic>? Characteristics { get; set; }
        public ICollection<OrderedProduct>? OrderedProducts { get; set; }
        public ICollection<PriceHistory>? PriceHistories { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Wishlist>? WhishLists { get; set; }


        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(p => p.Code)
                .HasDefaultValue(1000000)
                .ValueGeneratedOnAdd();

            builder.HasIndex(u => u.Name).IsUnique();

            builder.HasIndex(u => u.Url).IsUnique();

            builder.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Brands");

            builder.HasOne(d => d.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");

            builder.HasOne(d => d.Model)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Models");
        }
    }
}
