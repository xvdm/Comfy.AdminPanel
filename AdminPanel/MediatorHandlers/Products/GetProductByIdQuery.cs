using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products
{
    public class GetProductByIdQuery : IRequest<Product?>
    {
        public int ProductId { get; }
        public GetProductByIdQuery(int id)
        {
            ProductId = id;
        }
    }


    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly ApplicationDbContext _context;

        public GetProductByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(x => x.Characteristics)
                    .ThenInclude(x => x.CharacteristicsName)
                .Include(x => x.Characteristics)
                    .ThenInclude(x => x.CharacteristicsValue)
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.Model)
                .Include(x => x.Images)
                .Include(x => x.PriceHistory)
                .SingleOrDefaultAsync(x => x.Id == request.ProductId);

            return product;
        }
    }
}
