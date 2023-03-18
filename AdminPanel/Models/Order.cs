﻿using AdminPanel.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public partial class Order : IEntityTypeConfiguration<Order>
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int TotalSum { get; set; }
        public DateTime CreatingDate { get; set; }
        public DateTime ReceivingDate { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
        
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; } = null!;
        
        public int StatusId { get; set; }
        public OrderStatus Status { get; set; } = null!;

        public ICollection<Product> OrderedProducts { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CreatingDate).HasColumnType("date");
            builder.Property(e => e.Description).HasMaxLength(50);
            builder.Property(e => e.ReceivingDate).HasColumnType("date");

            builder.HasOne(d => d.Address);

            builder.HasOne(d => d.PaymentType);

            builder.HasOne(d => d.Status);

            builder
                .HasMany(x => x.OrderedProducts)
                .WithMany(x => x.Orders);
        }
    }
}
