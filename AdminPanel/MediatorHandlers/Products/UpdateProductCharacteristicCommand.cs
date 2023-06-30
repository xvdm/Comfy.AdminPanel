using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public sealed record UpdateProductCharacteristicCommand : IRequest
{
    public int ProductId { get; init; }
    public int CharacteristicId { get; init; }
    public string Name { get; init; }
    public string Value { get; init; }
    public UpdateProductCharacteristicCommand(int productId, int characteristicId, string name, string value)
    {
        ProductId = productId;
        CharacteristicId = characteristicId;
        Name = name.Trim();
        Value = value.Trim();
    }
}


public sealed class UpdateProductCharacteristicCommandHandler : IRequestHandler<UpdateProductCharacteristicCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public UpdateProductCharacteristicCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(UpdateProductCharacteristicCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(x => x.Category)
                .ThenInclude(x => x.UniqueCharacteristics)
            .Include(x => x.Characteristics)
                .ThenInclude(x => x.CharacteristicsName)
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null) throw new HttpRequestException("There is no product with given Id");
        if(product.Characteristics.Any(x => x.Id == request.CharacteristicId) == false) throw new HttpRequestException("There is no characteristic with given Id");

        if (product.Characteristics.Any(x => x.Id != request.CharacteristicId && x.CharacteristicsName.Name == request.Name))
        {
            throw new HttpRequestException("This product already has characteristic with given name");
        }

        var characteristicName = await _context.CharacteristicsNames.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        var characteristicValue = await _context.CharacteristicsValues.FirstOrDefaultAsync(x => x.Value == request.Value, cancellationToken);
        if (characteristicName is null)
        {
            characteristicName = new CharacteristicName { Name = request.Name };
            _context.CharacteristicsNames.Add(characteristicName);
        }
        if (characteristicValue is null)
        {
            characteristicValue = new CharacteristicValue { Value = request.Value };
            _context.CharacteristicsValues.Add(characteristicValue);
        }

        var characteristic = product.Characteristics.FirstOrDefault(x => x.Id == request.CharacteristicId);
        if (characteristic is not null)
        {
            await DeleteUniqueCharacteristicIfNoProductsLeft(product, characteristic, cancellationToken);

            characteristic.CharacteristicsName = characteristicName;
            characteristic.CharacteristicsValue = characteristicValue;

            await AddUniqueCharacteristicIfDoesNotExist(product, characteristicName, characteristicValue, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var productInvalidatedEvent = new ProductInvalidatedEvent(product.Id, product.Url);
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

    private async Task AddUniqueCharacteristicIfDoesNotExist(Product product, CharacteristicName characteristicName, CharacteristicValue characteristicValue, CancellationToken cancellationToken)
    {
        if (product.Category.UniqueCharacteristics.Any(x =>
                x.CharacteristicNameId == characteristicName.Id &&
                x.CharacteristicValueId == characteristicValue.Id)) return;

        await _context.SaveChangesAsync(cancellationToken);

        var categoryUniqueCharacteristic = new CategoryUniqueCharacteristic
        {
            SubcategoryId = product.CategoryId,
            CharacteristicNameId = characteristicName.Id,
            CharacteristicValueId = characteristicValue.Id
        };
        product.Category.UniqueCharacteristics.Add(categoryUniqueCharacteristic);
    }
}