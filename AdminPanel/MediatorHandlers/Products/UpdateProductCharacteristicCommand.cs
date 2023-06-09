﻿using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public sealed record UpdateProductCharacteristicCommand(int ProductId, int CharacteristicId, string Name, string Value) : IRequest;


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
        var characteristicCount = await _context.Characteristics.CountAsync(x => x.Id == request.CharacteristicId, cancellationToken);
        if (characteristicCount <= 0) throw new HttpRequestException("There is no characteristic with given Id");

        var product = await _context.Products
            .Include(x => x.Characteristics)
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null) throw new HttpRequestException("There is no product with given Id");

        var productCharacteristicWithName = product.Characteristics.Count(x => x.Id != request.CharacteristicId && x.CharacteristicsName.Name == request.Name);
        if (productCharacteristicWithName > 0)
        {
            throw new HttpRequestException("This product already has characteristic with given name");
        }

        var characteristicName = await _context.CharacteristicsNames
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

        var characteristicValue = await _context.CharacteristicsValues
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Value == request.Value, cancellationToken);

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
            characteristic.CharacteristicsName = characteristicName;
            characteristic.CharacteristicsValue = characteristicValue;
        }

        await _context.SaveChangesAsync(cancellationToken);

        var productInvalidatedEvent = new ProductInvalidatedEvent(product.Id, product.Url);
        await _publisher.Publish(productInvalidatedEvent, cancellationToken);
    }
}