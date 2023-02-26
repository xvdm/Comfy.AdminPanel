using AdminPanel.Commands.Products;
using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products
{
    public class EditProductCharacteristicCommandHandler : IRequestHandler<EditProductCharacteristicCommand>
    {
        private readonly ApplicationDbContext _context;

        public EditProductCharacteristicCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(EditProductCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var characteristic = await _context.Characteristics.FirstOrDefaultAsync(x => x.Id == request.CharacteristicId);
            if (characteristic is null) throw new HttpRequestException("There is no characteristic with given Id");

            var productCharacteristic = request.Product.Characteristics.FirstOrDefault(x => x.CharacteristicsName.Name == request.Name);
            if (productCharacteristic is not null && productCharacteristic.Id != request.CharacteristicId)
            {
                throw new HttpRequestException("This product already has characteristic with given name");
            }

            var characteristicName = await _context.CharacteristicsNames.FirstOrDefaultAsync(x => x.Name == request.Name);
            var characteristicValue = await _context.CharacteristicsValues.FirstOrDefaultAsync(x => x.Value == request.Value);
            if (characteristicName is null)
            {
                characteristicName = new CharacteristicName() { Name = request.Name };
                await _context.CharacteristicsNames.AddAsync(characteristicName);
            }
            if (characteristicValue is null)
            {
                characteristicValue = new CharacteristicValue() { Value = request.Value };
                await _context.CharacteristicsValues.AddAsync(characteristicValue);
            }
            request.Product.Characteristics.First(x => x.Id == request.CharacteristicId).CharacteristicsName = characteristicName;
            request.Product.Characteristics.First(x => x.Id == request.CharacteristicId).CharacteristicsValue = characteristicValue;

            await _context.SaveChangesAsync();
        }
    }
}
