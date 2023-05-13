using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public record DeleteProductCommand(int ProductId) : IRequest;


public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public DeleteProductCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(x => x.ShowcaseGroups)
            .Include(x => x.Category)
                .ThenInclude(x => x.UniqueBrands)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null) throw new HttpRequestException($"No product with was found with id {request.ProductId}");

        if(product.ShowcaseGroups.Count > 0) throw new HttpRequestException($"Product is in showcase group. Can not delete it");

        var priceHistories = await _context.PriceHistories
            .Where(x => x.ProductId == request.ProductId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        _context.PriceHistories.RemoveRange(priceHistories);
        _context.Products.Remove(product);

        var count = await _context.Products.CountAsync(x => x.BrandId == product.BrandId && x.CategoryId == product.CategoryId, cancellationToken);
        if (count <= 1) product.Category.UniqueBrands.Remove(product.Brand); // removing unique brand only if in the specified category there was one product left with this brandId

        await _context.SaveChangesAsync(cancellationToken);

        var productInvalidatedEvent = new ProductInvalidatedEvent(request.ProductId, product.Url);
        await _publisher.Publish(productInvalidatedEvent, cancellationToken);
    }
}