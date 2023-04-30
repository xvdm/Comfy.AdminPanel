using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public record DeleteProductCommand(int ProductId) : IRequest;


public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteProductCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null) throw new HttpRequestException($"No product with was found with id {request.ProductId}");

        var priceHistories = await _context.PriceHistories.Where(x => x.ProductId == request.ProductId).ToListAsync(cancellationToken);
        if (priceHistories.Any()) _context.PriceHistories.RemoveRange(priceHistories);

        _context.Products.Remove(product);

        await _context.SaveChangesAsync(cancellationToken);
    }
}