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
        await _context.SaveChangesAsync(cancellationToken);

        var productInvalidatedEvent = new ProductInvalidatedEvent(request.ProductId);
        await _publisher.Publish(productInvalidatedEvent, cancellationToken);
    }
}