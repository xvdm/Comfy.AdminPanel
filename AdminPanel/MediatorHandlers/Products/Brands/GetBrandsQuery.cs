using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.MediatorHandlers.Products.Brands
{
    public class GetBrandsQuery : IRequest<IEnumerable<Brand>>
    {
    }

    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, IEnumerable<Brand>>
    {
        private readonly ApplicationDbContext _context;

        public GetBrandsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Brands.ToListAsync();
        }
    }
}
