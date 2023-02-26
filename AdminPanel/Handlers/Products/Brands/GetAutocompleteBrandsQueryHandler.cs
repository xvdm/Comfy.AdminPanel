using AdminPanel.Data;
using AdminPanel.Queries.Products.Brands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Linq;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products.Brands
{
    public class GetAutocompleteBrandsQueryHandler : IRequestHandler<GetAutocompleteBrandsQuery, IQueryable<Brand>>
    {
        private readonly ApplicationDbContext _context;

        public GetAutocompleteBrandsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Brand>> Handle(GetAutocompleteBrandsQuery request, CancellationToken cancellationToken)
        {
            return _context.Brands.Where(x => x.Name.Contains(request.Input)).Take(request.Take);
        }
    }
}
