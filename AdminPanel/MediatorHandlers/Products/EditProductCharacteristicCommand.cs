using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products
{
    public record EditProductCharacteristicCommand(Product Product, int CharacteristicId, string Name, string Value) : IRequest;


    public class EditProductCharacteristicCommandHandler : IRequestHandler<EditProductCharacteristicCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPublisher _publisher;

        public EditProductCharacteristicCommandHandler(ApplicationDbContext context, IPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
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

            var productInvalidatedEvent = new ProductInvalidatedEvent(request.Product.Id);
            await _publisher.Publish(productInvalidatedEvent, cancellationToken);
        }
    }
}
