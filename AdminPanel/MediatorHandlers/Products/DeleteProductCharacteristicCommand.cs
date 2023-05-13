using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public record DeleteProductCharacteristicCommand(int ProductId, int CharacteristicId) : IRequest;


public class DeleteProductCharacteristicCommandHandler : IRequestHandler<DeleteProductCharacteristicCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public DeleteProductCharacteristicCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(DeleteProductCharacteristicCommand request, CancellationToken cancellationToken)
    {
        var characteristic = await _context.Characteristics
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.CharacteristicId, cancellationToken);
        if (characteristic is null) return;

        _context.Characteristics.Remove(characteristic);
        await _context.SaveChangesAsync(cancellationToken);

        var productUrl = await _context.Products.Select(x => new
        {
            x.Id,
            x.Url
        }).FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        var productInvalidatedEvent = new ProductInvalidatedEvent(request.ProductId, productUrl!.Url);
        await _publisher.Publish(productInvalidatedEvent, cancellationToken);
    }
}