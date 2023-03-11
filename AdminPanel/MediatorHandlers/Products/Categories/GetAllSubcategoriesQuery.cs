using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories
{
    public class GetAllSubcategoriesQuery : IRequest<IEnumerable<Subcategory>>
    {
    }

    public class GetAllSubcategoriesQueryHandler : IRequestHandler<GetAllSubcategoriesQuery, IEnumerable<Subcategory>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllSubcategoriesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subcategory>> Handle(GetAllSubcategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Subcategories.ToListAsync();
        }
    }
}
