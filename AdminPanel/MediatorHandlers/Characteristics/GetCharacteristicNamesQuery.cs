using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Characteristics;

public sealed record GetCharacteristicNamesQuery : IRequest<IEnumerable<CharacteristicName>>
{
    public string? SearchString { get; set; }

    private const int MaxPageSize = 20;
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

    public GetCharacteristicNamesQuery(string? searchString, int? pageSize, int? pageNumber)
    {
        SearchString = searchString;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}


public sealed class GetCharacteristicNamesQueryHandler : IRequestHandler<GetCharacteristicNamesQuery, IEnumerable<CharacteristicName>>
{
    private readonly ApplicationDbContext _context;

    public GetCharacteristicNamesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CharacteristicName>> Handle(GetCharacteristicNamesQuery request, CancellationToken cancellationToken)
    {
        var names = _context.CharacteristicsNames
            .AsQueryable();

        if (request.SearchString is not null)
        {
            names = names.Where(x => x.Name.Contains(request.SearchString));
        }

        return await names
            .OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}