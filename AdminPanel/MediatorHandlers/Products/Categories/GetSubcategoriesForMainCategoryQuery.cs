using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public sealed record GetSubcategoriesForMainCategoryQuery(int Id) : IRequest<IEnumerable<Subcategory>>;


public sealed class GetSubcategoriesForMainCategoryQueryHandler : IRequestHandler<GetSubcategoriesForMainCategoryQuery, IEnumerable<Subcategory>>
{
    private readonly ApplicationDbContext _context;

    public GetSubcategoriesForMainCategoryQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Subcategory>> Handle(GetSubcategoriesForMainCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Subcategories
            .Where(x => x.MainCategoryId == request.Id)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}