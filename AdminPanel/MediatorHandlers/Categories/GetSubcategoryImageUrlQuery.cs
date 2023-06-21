using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Categories;

public sealed record GetSubcategoryImageUrlQuery(int CategoryId) : IRequest<string?>;


public sealed class GetSubcategoryImageUrlQueryHandler : IRequestHandler<GetSubcategoryImageUrlQuery, string?>
{
    private readonly ApplicationDbContext _context;

    public GetSubcategoryImageUrlQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string?> Handle(GetSubcategoryImageUrlQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Subcategories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);
        return category?.ImageUrl;
    }
}