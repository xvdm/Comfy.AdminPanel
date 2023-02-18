using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class OrderStatus : IEntityTypeConfiguration<OrderStatus>
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public ICollection<Order>? Orders { get; set; }


        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Status).HasMaxLength(50);
        }
    }
}
