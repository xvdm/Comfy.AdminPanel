using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Categories
{
    public class GetSubcategoryFiltersQuery : IRequest<ICollection<SubcategoryFilter>>
    {
    }

    public class GetSubcategoryFiltersQueryHandler : IRequestHandler<GetSubcategoryFiltersQuery, ICollection<SubcategoryFilter>>
    {
        private readonly ApplicationDbContext _context;

        public GetSubcategoryFiltersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<SubcategoryFilter>> Handle(GetSubcategoryFiltersQuery request, CancellationToken cancellationToken)
        {
            return await _context.SubcategoryFilters
                .Include(x => x.Subcategory)
                .ToListAsync(cancellationToken);
        }
    }
}