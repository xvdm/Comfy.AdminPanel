using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.MediatorHandlers.Products.Categories
{
    public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
    {
    }

    public class GetBrandsQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ApplicationDbContext _context;

        public GetBrandsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
