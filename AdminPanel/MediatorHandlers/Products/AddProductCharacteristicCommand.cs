﻿using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public record AddProductCharacteristicCommand(int ProductId, string Name, string Value) : IRequest<Characteristic>;


public class AddProductCharacteristicCommandHandler : IRequestHandler<AddProductCharacteristicCommand, Characteristic>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public AddProductCharacteristicCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<Characteristic> Handle(AddProductCharacteristicCommand request, CancellationToken cancellationToken)
    {
        var characteristicsName = await _context.CharacteristicsNames.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        var characteristicsValue = await _context.CharacteristicsValues.FirstOrDefaultAsync(x => x.Value == request.Value, cancellationToken);

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
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null)
        {
            throw new HttpRequestException("Product with this id does not exist");
        }
        if (product.Characteristics.Count(x => x.CharacteristicsNameId == characteristicsName.Id) > 0)
        {
            throw new HttpRequestException("This product already has characteristic with this name");
        }


        var characteristic = new Characteristic()
        {
            CharacteristicsNameId = characteristicsName.Id,
            CharacteristicsValueId = characteristicsValue.Id,
            ProductId = request.ProductId
        };
        _context.Characteristics.Add(characteristic);
        product.Category.UniqueCharacteristics.Add(characteristic);
        await _context.SaveChangesAsync(cancellationToken);


        var productInvalidatedEvent = new ProductInvalidatedEvent(request.ProductId);
        await _publisher.Publish(productInvalidatedEvent, cancellationToken);

        return characteristic;
    }
}