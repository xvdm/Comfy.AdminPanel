﻿// <auto-generated />
using System;
using AdminPanel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AdminPanel.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230304151151_UniqueBrandsInCategory")]
    partial class UniqueBrandsInCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AdminPanel.Models.Identity.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("AdminPanel.Models.Identity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("AdminPanel.Models.Logging.LoggingAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LoggingActions");
                });

            modelBuilder.Entity("AdminPanel.Models.Logging.UserLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LoggingActionId")
                        .HasColumnType("int");

                    b.Property<Guid>("SubjectUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("LoggingActionId");

                    b.HasIndex("SubjectUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogs");
                });

            modelBuilder.Entity("AdminPanel.Models.MainCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("MainCategories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ApartmentsNumber")
                        .HasColumnType("int");

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressTypeId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("WebApplication2.Models.AddressType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("AddressTypes");
                });

            modelBuilder.Entity("WebApplication2.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("NeedlesslyAnswerCount")
                        .HasColumnType("int");

                    b.Property<int>("TargetId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsefullAnswerCount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TargetId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("WebApplication2.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("WebApplication2.Models.Characteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CharacteristicsNameId")
                        .HasColumnType("int");

                    b.Property<int>("CharacteristicsValueId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("SubcategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("SubcategoryId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicsNameId");

                    b.HasIndex("CharacteristicsValueId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SubcategoryId");

                    b.HasIndex("SubcategoryId1");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("WebApplication2.Models.CharacteristicName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CharacteristicsNames");
                });

            modelBuilder.Entity("WebApplication2.Models.CharacteristicValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CharacteristicsValues");
                });

            modelBuilder.Entity("WebApplication2.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("WebApplication2.Models.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("longtext")
                        .HasColumnName("Model");

                    b.HasKey("Id");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("WebApplication2.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatingDate")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ProductCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReceivingDate")
                        .HasColumnType("date");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("TotalSum")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApplication2.Models.OrderedProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderedProducts");
                });

            modelBuilder.Entity("WebApplication2.Models.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("WebApplication2.Models.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("WebApplication2.Models.PriceHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("PriceHistory", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("DiscountAmmount")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .HasColumnType("double");

                    b.Property<string>("Url")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("ModelId");

                    b.HasIndex("Name");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebApplication2.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("NeedlesslyCount")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsefullQuestionCount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("WebApplication2.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Advantages")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<string>("Disadvantages")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("NeedlesslyReviewCount")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("ProductRating")
                        .HasColumnType("double");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsefullReviewCount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("WebApplication2.Models.Subcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MainCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryId");

                    b.ToTable("Subcategories");
                });

            modelBuilder.Entity("WebApplication2.Models.Wishlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("WhishLists");
                });

            modelBuilder.Entity("AdminPanel.Models.Logging.UserLog", b =>
                {
                    b.HasOne("AdminPanel.Models.Logging.LoggingAction", "LoggingAction")
                        .WithMany()
                        .HasForeignKey("LoggingActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminPanel.Models.Identity.ApplicationUser", "SubjectUser")
                        .WithMany()
                        .HasForeignKey("SubjectUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminPanel.Models.Identity.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoggingAction");

                    b.Navigation("SubjectUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("AdminPanel.Models.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("AdminPanel.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("AdminPanel.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("AdminPanel.Models.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminPanel.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("AdminPanel.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication2.Models.Address", b =>
                {
                    b.HasOne("WebApplication2.Models.AddressType", "AddressType")
                        .WithMany("Addresses")
                        .HasForeignKey("AddressTypeId")
                        .IsRequired()
                        .HasConstraintName("FK_Addresses_AddressTypes");

                    b.Navigation("AddressType");
                });

            modelBuilder.Entity("WebApplication2.Models.Answer", b =>
                {
                    b.HasOne("WebApplication2.Models.Question", "TargetQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("TargetId")
                        .IsRequired()
                        .HasConstraintName("FK_Answers_Questions");

                    b.HasOne("WebApplication2.Models.Review", "TargetReview")
                        .WithMany("Answers")
                        .HasForeignKey("TargetId")
                        .IsRequired()
                        .HasConstraintName("FK_Answers_Reviews");

                    b.Navigation("TargetQuestion");

                    b.Navigation("TargetReview");
                });

            modelBuilder.Entity("WebApplication2.Models.Characteristic", b =>
                {
                    b.HasOne("WebApplication2.Models.CharacteristicName", "CharacteristicsName")
                        .WithMany("Characteristics")
                        .HasForeignKey("CharacteristicsNameId")
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.CharacteristicValue", "CharacteristicsValue")
                        .WithMany("Characteristics")
                        .HasForeignKey("CharacteristicsValueId")
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.Product", "Product")
                        .WithMany("Characteristics")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Characteristics_Products");

                    b.HasOne("WebApplication2.Models.Subcategory", null)
                        .WithMany("UniqueBrands")
                        .HasForeignKey("SubcategoryId");

                    b.HasOne("WebApplication2.Models.Subcategory", null)
                        .WithMany("UniqueCharacteristics")
                        .HasForeignKey("SubcategoryId1");

                    b.Navigation("CharacteristicsName");

                    b.Navigation("CharacteristicsValue");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApplication2.Models.Image", b =>
                {
                    b.HasOne("WebApplication2.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApplication2.Models.Order", b =>
                {
                    b.HasOne("WebApplication2.Models.Address", "Address")
                        .WithMany("Orders")
                        .HasForeignKey("AddressId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Addresses");

                    b.HasOne("WebApplication2.Models.PaymentType", "PaymentType")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentTypeId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_PaymentTypes");

                    b.HasOne("WebApplication2.Models.OrderStatus", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_OrderStatuses");

                    b.Navigation("Address");

                    b.Navigation("PaymentType");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WebApplication2.Models.OrderedProduct", b =>
                {
                    b.HasOne("WebApplication2.Models.Order", "Order")
                        .WithMany("OrderedProducts")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderedProducts_Orders");

                    b.HasOne("WebApplication2.Models.Product", "Product")
                        .WithMany("OrderedProducts")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderedProducts_Products");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApplication2.Models.PriceHistory", b =>
                {
                    b.HasOne("WebApplication2.Models.Product", "Product")
                        .WithMany("PriceHistory")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_PriceHistory_Products");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApplication2.Models.Product", b =>
                {
                    b.HasOne("WebApplication2.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.Subcategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.Model", "Model")
                        .WithMany("Products")
                        .HasForeignKey("ModelId")
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("WebApplication2.Models.Question", b =>
                {
                    b.HasOne("WebApplication2.Models.Product", "Product")
                        .WithMany("Questions")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Questions_Products");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApplication2.Models.Review", b =>
                {
                    b.HasOne("WebApplication2.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Reviews_Products");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApplication2.Models.Subcategory", b =>
                {
                    b.HasOne("AdminPanel.Models.MainCategory", "MainCategory")
                        .WithMany("Categories")
                        .HasForeignKey("MainCategoryId")
                        .IsRequired();

                    b.Navigation("MainCategory");
                });

            modelBuilder.Entity("WebApplication2.Models.Wishlist", b =>
                {
                    b.HasOne("WebApplication2.Models.Product", "Product")
                        .WithMany("WhishLists")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_WhishLists_Products");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AdminPanel.Models.MainCategory", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("WebApplication2.Models.Address", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("WebApplication2.Models.AddressType", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("WebApplication2.Models.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebApplication2.Models.CharacteristicName", b =>
                {
                    b.Navigation("Characteristics");
                });

            modelBuilder.Entity("WebApplication2.Models.CharacteristicValue", b =>
                {
                    b.Navigation("Characteristics");
                });

            modelBuilder.Entity("WebApplication2.Models.Model", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebApplication2.Models.Order", b =>
                {
                    b.Navigation("OrderedProducts");
                });

            modelBuilder.Entity("WebApplication2.Models.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("WebApplication2.Models.PaymentType", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("WebApplication2.Models.Product", b =>
                {
                    b.Navigation("Characteristics");

                    b.Navigation("Images");

                    b.Navigation("OrderedProducts");

                    b.Navigation("PriceHistory");

                    b.Navigation("Questions");

                    b.Navigation("Reviews");

                    b.Navigation("WhishLists");
                });

            modelBuilder.Entity("WebApplication2.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("WebApplication2.Models.Review", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("WebApplication2.Models.Subcategory", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("UniqueBrands");

                    b.Navigation("UniqueCharacteristics");
                });
#pragma warning restore 612, 618
        }
    }
}
