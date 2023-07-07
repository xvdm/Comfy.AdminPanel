using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Categories;

public sealed record GetSubcategoryFiltersTotalCountQuery(string? SearchString) : IRequest<int>;


public sealed class GetSubcategoryFiltersTotalCountQueryHandler : IRequestHandler<GetSubcategoryFiltersTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetSubcategoryFiltersTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetSubcategoryFiltersTotalCountQuery request, CancellationToken cancellationToken)
    {
        var filters = _context.SubcategoryFilters.AsQueryable();

        if (request.SearchString is not null)
        {
            filters = filters.Where(x => x.Name.Contains(request.SearchString));
        }

        return await filters.CountAsync(cancellationToken);
    }
}