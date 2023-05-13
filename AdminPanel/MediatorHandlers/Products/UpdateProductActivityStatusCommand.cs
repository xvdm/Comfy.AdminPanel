using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public record UpdateProductActivityStatusCommand(int ProductId, bool IsActive) : IRequest;


public class UpdateProductActivityStatusCommandHandler : IRequestHandler<UpdateProductActivityStatusCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public UpdateProductActivityStatusCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(UpdateProductActivityStatusCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null) throw new HttpRequestException("Product was not found");

        product.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);

        var productInvalidatedEvent = new ProductInvalidatedEvent(request.ProductId, product.Url);
        await _publisher.Publish(productInvalidatedEvent, cancellationToken);
    }
}