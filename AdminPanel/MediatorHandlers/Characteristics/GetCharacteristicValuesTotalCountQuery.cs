using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Characteristics;

public sealed record GetCharacteristicValuesTotalCountQuery(string? SearchString) : IRequest<int>;


public sealed class GetCharacteristicValuesTotalCountQueryHandler : IRequestHandler<GetCharacteristicValuesTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetCharacteristicValuesTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetCharacteristicValuesTotalCountQuery request, CancellationToken cancellationToken)
    {
        var values = _context.CharacteristicsValues.AsQueryable();
        if (string.IsNullOrWhiteSpace(request.SearchString) == false) values = values.Where(x => x.Value.Contains(request.SearchString));
        return await values.CountAsync(cancellationToken);
    }
}