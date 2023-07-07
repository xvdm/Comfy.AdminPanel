using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Characteristics;

public sealed record GetCharacteristicNamesTotalCountQuery(string? SearchString) : IRequest<int>;


public sealed class GetCharacteristicNamesTotalCountQueryHandler : IRequestHandler<GetCharacteristicNamesTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetCharacteristicNamesTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetCharacteristicNamesTotalCountQuery request, CancellationToken cancellationToken)
    {
        var names = _context.CharacteristicsNames.AsQueryable();
        if (request.SearchString is not null) names = names.Where(x => x.Name.Contains(request.SearchString));
        return await names.CountAsync(cancellationToken);
    }
}