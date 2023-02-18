using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Wishlist : IEntityTypeConfiguration<Wishlist>
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        //public ApplicationUser User { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Product)
                    .WithMany(p => p.WhishLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WhishLists_Products");
        }
    }
}
