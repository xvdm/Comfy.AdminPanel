using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.MediatorHandlers.Products.Categories
{
    public class GetSubcategoryByIdQuery : IRequest<Subcategory?>
    {
        public int? CategoryId { get; set; }
        public GetSubcategoryByIdQuery(int? categoryId)
        {
            CategoryId = categoryId;
        }
    }

    public class GetSubcategoryByIdQueryHandler : IRequestHandler<GetSubcategoryByIdQuery, Subcategory?>
    {
        private readonly ApplicationDbContext _context;

        public GetSubcategoryByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Subcategory?> Handle(GetSubcategoryByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.CategoryId is null) return null;

            var category = await _context.Subcategories
                .Where(x => x.Id == request.CategoryId)
                .Include(x => x.UniqueCharacteristics)
                    .ThenInclude(x => x.CharacteristicsName)
                .Include(x => x.UniqueCharacteristics)
                    .ThenInclude(x => x.CharacteristicsValue)
                .FirstOrDefaultAsync();

            return category;
        }
    }
}
