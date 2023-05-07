using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Helpers;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public record UpdateProductCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Price { get; set; }
    public int Brand { get; set; }
    public int Category { get; set; }
    public int Model { get; set; }
    public string Description { get; set; } = null!;
    public int DiscountAmount { get; set; }
}


public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public UpdateProductCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(x => x.Brand)
            .Include(x => x.Category)
            .Include(x => x.Model)
            .Include(x => x.PriceHistory)
            .Include(x => x.ShowcaseGroups)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null) throw new HttpRequestException("Product was not found");

        if (product.Brand.Id != request.Brand)
        {
            var brand = await _context.Brands
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Brand, cancellationToken);
            if (brand is null) throw new HttpRequestException("This brand does not exist");
            product.Brand = brand;
        }
        if (product.Category.Id != request.Category)
        {
            var category = await _context.Subcategories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Category, cancellationToken);
            if (category is null) throw new HttpRequestException("This category does not exist");
            product.Category = category;
        }
        if (product.Model.Id != request.Model)
        {
            var model = await _context.Models
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Model, cancellationToken);
            if (model is null) throw new HttpRequestException("This model does not exist");
            product.Model = model;
        }


        if (product.Name != request.Name)
        {
            product.Name = request.Name;
            product.Url = ProductUrl.Create(product.Name, product.Code);
        }

        if (product.Price != request.Price)
        {
            product.Price = request.Price;
            var priceHistory = new PriceHistory
            {
                Date = DateTime.Now,
                Price = product.Price,
                ProductId = product.Id
            };
            product.PriceHistory.Add(priceHistory);
        }

        product.Description = request.Description;
        product.DiscountAmount = request.DiscountAmount;

        await _context.SaveChangesAsync(cancellationToken);

        var productInvalidatedEvent = new ProductInvalidatedEvent(request.Id);
        await _publisher.Publish(productInvalidatedEvent, cancellationToken);

        if (product.ShowcaseGroups.Count > 0)
        {
            var notification = new ShowcaseGroupsInvalidatedEvent();
            await _publisher.Publish(notification, cancellationToken);
        }

        return product.Id;
    }
}