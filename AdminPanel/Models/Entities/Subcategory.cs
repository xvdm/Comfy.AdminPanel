﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models.Entities;

public sealed class Subcategory : IEntityTypeConfiguration<Subcategory>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? ImageUrl { get; set; }

    public int MainCategoryId { get; set; }
    public MainCategory MainCategory { get; set; } = null!;

    public ISet<CategoryUniqueCharacteristic> UniqueCharacteristics { get; set; } = null!;
    public ISet<Brand> UniqueBrands { get; set; } = null!;
    public ICollection<SubcategoryFilter> Filters { get; set; } = null!;

    public void Configure(EntityTypeBuilder<Subcategory> builder)
    {
        builder.HasOne(x => x.MainCategory)
            .WithMany(x => x.Categories)
            .HasForeignKey(x => x.MainCategoryId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}