using AdminPanel.Data;
using MediatR;
using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Handlers.Products.Brands
{
    public class CreateBrandCommand : IRequest<Brand>
    {
        public Brand Brand { get; set; }

        public CreateBrandCommand(Brand brand)
        {
            Brand = brand;
        }
    }


    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Brand>
    {
        private readonly ApplicationDbContext _context;

        public CreateBrandCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brandWithName = await _context.Brands.FirstOrDefaultAsync(x => x.Name == request.Brand.Name, cancellationToken);
            if (brandWithName is not null) throw new HttpRequestException($"Brand with name {request.Brand.Name} already exists");
            await _context.Brands.AddAsync(request.Brand, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Brand;
        }
    }
}
