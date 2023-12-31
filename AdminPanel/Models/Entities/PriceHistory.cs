﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models.Entities;

public sealed class PriceHistory : IEntityTypeConfiguration<PriceHistory>
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;


    public void Configure(EntityTypeBuilder<PriceHistory> builder)
    {
        builder.HasOne(d => d.Product)
            .WithMany(p => p.PriceHistory)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}