using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public sealed record GetMainCategoriesQuery : IRequest<IEnumerable<MainCategory>>;


public sealed class GetMainCategoriesQueryHandler : IRequestHandler<GetMainCategoriesQuery, IEnumerable<MainCategory>>
{
    private readonly ApplicationDbContext _context;

    public GetMainCategoriesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MainCategory>> Handle(GetMainCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _context.MainCategories
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }
}