﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Models.Logging;
using System.Reflection;
using AdminPanel.Models.Identity;
using AdminPanel.Models;

namespace AdminPanel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserLog> UserLogs { get; set; } = null!;
        public DbSet<LoggingAction> LoggingActions { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<AddressType> AddressTypes { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<MainCategory> MainCategories { get; set; } = null!;
        public DbSet<Subcategory> Subcategories { get; set; } = null!;
        public DbSet<Characteristic> Characteristics { get; set; } = null!;
        public DbSet<CharacteristicName> CharacteristicsNames { get; set; } = null!;
        public DbSet<CharacteristicValue> CharacteristicsValues { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Model> Models { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public DbSet<PriceHistory> PriceHistories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<ReviewAnswer> ReviewAnswers { get; set; } = null!;
        public DbSet<Wishlist> WhishLists { get; set; } = null!;
        public DbSet<MainCategoryImage> MainCategoryImages { get; set; } = null!;
        public DbSet<SubcategoryImage> SubcategoryImages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
