using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Models;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record GetSubcategoryByIdQuery(int? CategoryId) : IRequest<Subcategory?>;


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
            .FirstOrDefaultAsync(cancellationToken);

        return category;
    }
}