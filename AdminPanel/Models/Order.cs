﻿using AdminPanel.Models.Base;
using AdminPanel.Models.Identity;

namespace AdminPanel.Models
{
    public class Order : Auditable
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int TotalSum { get; set; }
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
    }
}
