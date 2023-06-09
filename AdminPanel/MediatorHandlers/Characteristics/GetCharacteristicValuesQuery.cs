using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Characteristics;

public sealed record GetCharacteristicValuesQuery : IRequest<IEnumerable<CharacteristicValue>>
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

    public GetCharacteristicValuesQuery(string? searchString, int? pageSize, int? pageNumber)
    {
        SearchString = searchString;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}


public sealed class GetCharacteristicValuesQueryHandler : IRequestHandler<GetCharacteristicValuesQuery, IEnumerable<CharacteristicValue>>
{
    private readonly ApplicationDbContext _context;

    public GetCharacteristicValuesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CharacteristicValue>> Handle(GetCharacteristicValuesQuery request, CancellationToken cancellationToken)
    {
        var values = _context.CharacteristicsValues
            .AsQueryable();

        if (request.SearchString is not null)
        {
            values = values.Where(x => x.Value.Contains(request.SearchString));
        }

        return await values
            .OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}