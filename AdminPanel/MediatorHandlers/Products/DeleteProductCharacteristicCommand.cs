using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Handlers.Products
{
    public class DeleteProductCharacteristicCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteProductCharacteristicCommand(int id)
        {
            Id = id;
        }
    }


    public class DeleteProductCharacteristicCommandHandler : IRequestHandler<DeleteProductCharacteristicCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteProductCharacteristicCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProductCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var characteristic = await _context.Characteristics.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (characteristic is null) throw new HttpRequestException("There is no characteristic with given Id");
            _context.Characteristics.Remove(characteristic);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
