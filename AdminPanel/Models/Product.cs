﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
        public Subcategory Category { get; set; } = null!;

        public int ModelId { get; set; }
        public Model Model { get; set; } = null!;

        public ICollection<PriceHistory> PriceHistory { get; set; } = null!;
        public ICollection<Image> Images { get; set; } = null!;

        public ICollection<Characteristic> Characteristics { get; set; } = null!;
        public ICollection<OrderedProduct> OrderedProducts { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = null!;
        public ICollection<Wishlist> WhishLists { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name);

            builder.HasIndex(x => x.Code).IsUnique();

            builder.HasIndex(x => x.Url).IsUnique();

            builder.HasOne(x => x.Brand)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Model)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}