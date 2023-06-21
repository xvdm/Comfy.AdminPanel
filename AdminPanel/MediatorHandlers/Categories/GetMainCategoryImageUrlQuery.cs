using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Categories;

public sealed record GetMainCategoryImageUrlQuery(int CategoryId) : IRequest<string?>;


public sealed class GetMainCategoryImageUrlQueryHandler : IRequestHandler<GetMainCategoryImageUrlQuery, string?>
{
    private readonly ApplicationDbContext _context;

    public GetMainCategoryImageUrlQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string?> Handle(GetMainCategoryImageUrlQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.MainCategories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);
        return category?.ImageUrl;
    }
}