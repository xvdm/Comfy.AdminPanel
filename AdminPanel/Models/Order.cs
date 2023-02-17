using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Order : IEntityTypeConfiguration<Order>
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int TotalSum { get; set; }
        public DateTime CreatingDate { get; set; }
        public DateTime ReceivingDate { get; set; }
        public int ProductCount { get; set; }

        public int UserId { get; set; }
        //public ApplicationUser User { get; set; } = null!;
        
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
        
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; } = null!;
        
        public int StatusId { get; set; }
        public OrderStatus Status { get; set; } = null!;

        public ICollection<OrderedProduct>? OrderedProducts { get; set; }


        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CreatingDate).HasColumnType("date");
            builder.Property(e => e.Description).HasMaxLength(50);
            builder.Property(e => e.ReceivingDate).HasColumnType("date");

            builder.HasOne(d => d.Address)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Addresses");

            builder.HasOne(d => d.PaymentType)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_PaymentTypes");

            builder.HasOne(d => d.Status)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_OrderStatuses");
        }
    }
}
