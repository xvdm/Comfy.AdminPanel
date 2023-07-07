using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Categories;

public sealed record GetSubcategoryFiltersQuery : IRequest<ICollection<SubcategoryFilter>>
{
    public string? SearchString { get; set; }

    private const int MaxPageSize = 10;
    private int _pageSize = MaxPageSize;
    private int _pageNumber = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value is > MaxPageSize or < 1 ? MaxPageSize : value;
    }
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = (value < 1) ? 1 : value;
    }

    public GetSubcategoryFiltersQuery(string? searchString, int? pageSize, int? pageNumber)
    {
        SearchString = searchString;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}

public sealed class GetSubcategoryFiltersQueryHandler : IRequestHandler<GetSubcategoryFiltersQuery, ICollection<SubcategoryFilter>>
{
    private readonly ApplicationDbContext _context;

    public GetSubcategoryFiltersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<SubcategoryFilter>> Handle(GetSubcategoryFiltersQuery request, CancellationToken cancellationToken)
    {
        var filters = _context.SubcategoryFilters
            .Include(x => x.Subcategory)
            .AsQueryable();

        if(request.SearchString is not null)
        {
            filters = filters.Where(x => x.Name.Contains(request.SearchString));
        }

        return await filters
            .OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}