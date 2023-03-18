using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Models;

namespace AdminPanel.Handlers.Products
{
    public class EditProductCharacteristicCommand : IRequest
    {
        public Product Product { get; set; }
        public int CharacteristicId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public EditProductCharacteristicCommand(Product product, int characteristicId, string name, string value)
        {
            Product = product;
            CharacteristicId = characteristicId;
            Name = name;
            Value = value;
        }
    }


    public class EditProductCharacteristicCommandHandler : IRequestHandler<EditProductCharacteristicCommand>
    {
        private readonly ApplicationDbContext _context;

        public EditProductCharacteristicCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(EditProductCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var characteristic = await _context.Characteristics.FirstOrDefaultAsync(x => x.Id == request.CharacteristicId, cancellationToken);
            if (characteristic is null) throw new HttpRequestException("There is no characteristic with given Id");

            var productCharacteristic = request.Product.Characteristics.FirstOrDefault(x => x.CharacteristicsName.Name == request.Name);
            if (productCharacteristic is not null && productCharacteristic.Id != request.CharacteristicId)
            {
                throw new HttpRequestException("This product already has characteristic with given name");
            }

            var characteristicName = await _context.CharacteristicsNames.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            var characteristicValue = await _context.CharacteristicsValues.FirstOrDefaultAsync(x => x.Value == request.Value, cancellationToken);
            if (characteristicName is null)
            {
                characteristicName = new CharacteristicName() { Name = request.Name };
                await _context.CharacteristicsNames.AddAsync(characteristicName, cancellationToken);
            }
            if (characteristicValue is null)
            {
                characteristicValue = new CharacteristicValue() { Value = request.Value };
                await _context.CharacteristicsValues.AddAsync(characteristicValue, cancellationToken);
            }
            request.Product.Characteristics.First(x => x.Id == request.CharacteristicId).CharacteristicsName = characteristicName;
            request.Product.Characteristics.First(x => x.Id == request.CharacteristicId).CharacteristicsValue = characteristicValue;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
