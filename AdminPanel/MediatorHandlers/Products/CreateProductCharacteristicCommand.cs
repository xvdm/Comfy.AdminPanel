using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public sealed record CreateProductCharacteristicCommand : IRequest<Characteristic>
{
    public int ProductId { get; init; }
    public int GroupId { get; init; }
    public string Name { get; init; }
    public string Value { get; init; }
    public CreateProductCharacteristicCommand(int productId, string name, string value, int groupId)
    {
        ProductId = productId;
        Name = name.Trim();
        Value = value.Trim();
        GroupId = groupId;
    }
}


public sealed class CreateProductCharacteristicCommandHandler : IRequestHandler<CreateProductCharacteristicCommand, Characteristic>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public CreateProductCharacteristicCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<Characteristic> Handle(CreateProductCharacteristicCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.CharacteristicGroups.FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if(group is null) throw new HttpRequestException("Group with this id does not exist");
        if(group.ProductId != request.ProductId) throw new HttpRequestException("Group is not for this product");

        var characteristicsName = await _context.CharacteristicsNames
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
       
        var characteristicsValue = await _context.CharacteristicsValues
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Value == request.Value, cancellationToken);

        var isNewCharacteristic = false;
        if (characteristicsName is null)
        {
            characteristicsName = new CharacteristicName { Name = request.Name };
            _context.CharacteristicsNames.Add(characteristicsName);
            isNewCharacteristic = true;
        }
        if (characteristicsValue is null)
        {
            characteristicsValue = new CharacteristicValue { Value = request.Value };
            _context.CharacteristicsValues.Add(characteristicsValue);
            isNewCharacteristic = true;
        }
        if (isNewCharacteristic)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        var product = await _context.Products
            .Include(x => x.Characteristics)
                .ThenInclude(x => x.CharacteristicsName)
            .Include(x => x.Category)
                .ThenInclude(x => x.UniqueCharacteristics)
                    .ThenInclude(x => x.CharacteristicName)
            .Include(x => x.Category)
                .ThenInclude(x => x.UniqueCharacteristics)
                    .ThenInclude(x => x.CharacteristicValue)
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null)
        {
            throw new HttpRequestException("Product with this id does not exist");
        }
        if (product.Characteristics.Count(x => x.CharacteristicsNameId == characteristicsName.Id) > 0)
        {
            throw new HttpRequestException("This product already has characteristic with this name");
        }


        var characteristic = new Characteristic
        {
            CharacteristicsNameId = characteristicsName.Id,
            CharacteristicsValueId = characteristicsValue.Id,
            ProductId = request.ProductId,
            CharacteristicGroupId = request.GroupId
        };
        _context.Characteristics.Add(characteristic);


        if (product.Category.UniqueCharacteristics.Any(x => 
                x.CharacteristicNameId == characteristicsName.Id &&
                x.CharacteristicValueId == characteristicsValue.Id) == false)
        {
            var categoryUniqueCharacteristic = new CategoryUniqueCharacteristic
            {
                SubcategoryId = product.CategoryId,
                CharacteristicNameId = characteristicsName.Id,
                CharacteristicValueId = characteristicsValue.Id
            };
            product.Category.UniqueCharacteristics.Add(categoryUniqueCharacteristic);
        }

        await _context.SaveChangesAsync(cancellationToken);


        var productInvalidatedEvent = new ProductInvalidatedEvent(request.ProductId, product.Url);
        await _publisher.Publish(productInvalidatedEvent, cancellationToken);

        return characteristic;
    }
}