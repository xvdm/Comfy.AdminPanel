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
        var characteristic = await _context.Characteristics.FirstOrDefaultAsync(x => x.Id == request.CharacteristicId, cancellationToken);
        if (characteristic is null) throw new HttpRequestException("There is no characteristic with given Id");
        _context.Characteristics.Remove(characteristic);
        await _context.SaveChangesAsync(cancellationToken);

        var productInvalidatedEvent = new ProductInvalidatedEvent(request.ProductId);
        await _publisher.Publish(productInvalidatedEvent, cancellationToken);
    }
}