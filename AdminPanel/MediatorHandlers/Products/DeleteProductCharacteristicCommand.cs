using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public sealed record DeleteProductCharacteristicCommand(int ProductId, int CharacteristicId) : IRequest;


public sealed class DeleteProductCharacteristicCommandHandler : IRequestHandler<DeleteProductCharacteristicCommand>
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
            .Include(x => x.CharacteristicsName)
            .Include(x => x.CharacteristicsValue)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.CharacteristicId, cancellationToken);
        if (characteristic is null) return;

        var product = await _context.Products
            .Include(x => x.Category)
                .ThenInclude(x => x.UniqueCharacteristics)
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null) return;

        await DeleteUniqueCharacteristicIfNoProductsLeft(product, characteristic, cancellationToken);

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

    private async Task DeleteUniqueCharacteristicIfNoProductsLeft(Product product, Characteristic characteristic, CancellationToken cancellationToken)
    {
        var productsWithThisCharacteristic = await _context.Characteristics
            .Include(x => x.Product)
            .CountAsync(x => x.Product.CategoryId == product.CategoryId &&
                             x.CharacteristicsNameId == characteristic.CharacteristicsNameId &&
                             x.CharacteristicsValueId == characteristic.CharacteristicsValueId, cancellationToken);
        if (productsWithThisCharacteristic <= 1)
        {
            var uniqueCharacteristic = await _context.CategoryUniqueCharacteristics
                .FirstOrDefaultAsync(x => x.SubcategoryId == product.CategoryId &&
                                          x.CharacteristicNameId == characteristic.CharacteristicsNameId &&
                                          x.CharacteristicValueId == characteristic.CharacteristicsValueId, cancellationToken);
            if (uniqueCharacteristic is null) return;
            product.Category.UniqueCharacteristics.Remove(uniqueCharacteristic);
        }
    }
}