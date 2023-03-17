using AdminPanel.Data;
using MediatR;
using AdminPanel.Models;

namespace AdminPanel.Handlers.Products.Brands
{
    public class CreateBrandCommand : IRequest
    {
        public Brand Brand { get; set; }

        public CreateBrandCommand(Brand brand)
        {
            Brand = brand;
        }
    }


    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateBrandCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _context.Brands.AddAsync(request.Brand, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
