using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public record AddProductCharacteristicCommand(int ProductId, string Name, string Value) : IRequest<Characteristic>;


public class AddProductCharacteristicCommandHandler : IRequestHandler<AddProductCharacteristicCommand, Characteristic>
{
    private readonly ApplicationDbContext _context;

    public AddProductCharacteristicCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Characteristic> Handle(AddProductCharacteristicCommand request, CancellationToken cancellationToken)
    {
        var characteristicsName = await _context.CharacteristicsNames.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        var characteristicsValue = await _context.CharacteristicsValues.FirstOrDefaultAsync(x => x.Value == request.Value, cancellationToken);

        var isNewCharacteristic = false;
        if (characteristicsName is null)
        {
            characteristicsName = new CharacteristicName() { Name = request.Name };
            await _context.CharacteristicsNames.AddAsync(characteristicsName, cancellationToken);
            isNewCharacteristic = true;
        }
        if (characteristicsValue is null)
        {
            characteristicsValue = new CharacteristicValue() { Value = request.Value };
            await _context.CharacteristicsValues.AddAsync(characteristicsValue, cancellationToken);
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
        if (product.Characteristics.FirstOrDefault(x => x.CharacteristicsNameId == characteristicsName.Id) is not null)
        {
            throw new HttpRequestException("This product already has characteristic with this name");
        }


        var characteristic = new Characteristic()
        {
            CharacteristicsNameId = characteristicsName.Id,
            CharacteristicsValueId = characteristicsValue.Id,
            ProductId = request.ProductId
        };
        await _context.Characteristics.AddAsync(characteristic, cancellationToken);
        product.Category.UniqueCharacteristics.Add(characteristic);
        await _context.SaveChangesAsync(cancellationToken);

        return characteristic;
    }
}