using AdminPanel.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models.Entities;

public sealed class Product : Auditable, IEntityTypeConfiguration<Product>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int DiscountAmount { get; set; }
    public int Amount { get; set; }
    public int Code { get; set; }
    public double Rating { get; set; }
    public int ReviewsNumber { get; set; }
    public bool IsActive { get; set; }
    public string Url { get; set; } = null!;

    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;

    public int CategoryId { get; set; }
    public Subcategory Category { get; set; } = null!;

    public int ModelId { get; set; }
    public Model Model { get; set; } = null!;

    public ICollection<PriceHistory> PriceHistory { get; set; } = null!;
    public IList<Image> Images { get; set; } = null!;

    public ICollection<Characteristic> Characteristics { get; set; } = null!;
    public ICollection<CharacteristicGroup> CharacteristicGroups { get; set; } = null!;

    public ICollection<Question> Questions { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = null!;

    public ICollection<WishList> WishLists { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = null!;
    public ICollection<ShowcaseGroup> ShowcaseGroups { get; set; } = null!;

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => x.Url).IsUnique();

        builder.HasMany(x => x.Orders).WithMany(x => x.Products);
        builder.HasMany(x => x.WishLists).WithMany(x => x.Products);
    }
}