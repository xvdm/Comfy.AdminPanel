using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Brands
{
    public class UpdateBrandCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UpdateBrandCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateBrandCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brandWithName = await _context.Brands.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (brandWithName?.Id != request.Id)
            {
                if (brandWithName is not null) throw new HttpRequestException($"Brand with name {request.Name} already exists");
                var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (brand is null) throw new HttpRequestException($"Brand with id {request.Id} was not found");
                brand.Name = request.Name;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
