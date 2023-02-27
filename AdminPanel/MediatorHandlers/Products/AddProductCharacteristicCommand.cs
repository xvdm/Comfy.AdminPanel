using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products
{
    public class AddProductCharacteristicCommand : IRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }


    public class AddProductCharacteristicCommandHandler : IRequestHandler<AddProductCharacteristicCommand>
    {
        private readonly ApplicationDbContext _context;

        public AddProductCharacteristicCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AddProductCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var characteristicsName = await _context.CharacteristicsNames.FirstOrDefaultAsync(x => x.Name == request.Name);
            var characteristicsValue = await _context.CharacteristicsValues.FirstOrDefaultAsync(x => x.Value == request.Value);

            bool isNewCharacteristic = false;
            if (characteristicsName is null)
            {
                characteristicsName = new CharacteristicName() { Name = request.Name };
                await _context.CharacteristicsNames.AddAsync(characteristicsName);
                isNewCharacteristic = true;
            }
            if (characteristicsValue is null)
            {
                characteristicsValue = new CharacteristicValue() { Value = request.Value };
                await _context.CharacteristicsValues.AddAsync(characteristicsValue);
                isNewCharacteristic = true;
            }
            if (isNewCharacteristic)
            {
                await _context.SaveChangesAsync();
            }

            var product = await _context.Products
                .Include(x => x.Characteristics)
                .ThenInclude(x => x.CharacteristicsName)
                .FirstOrDefaultAsync(x => x.Id == request.ProductId);
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
            await _context.Characteristics.AddAsync(characteristic);
            await _context.SaveChangesAsync();
        }
    }
}
