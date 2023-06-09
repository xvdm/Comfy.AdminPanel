using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Characteristics;

public sealed record GetCharacteristicNamesTotalCountQuery : IRequest<int>;


public sealed class GetCharacteristicNamesTotalCountQueryHandler : IRequestHandler<GetCharacteristicNamesTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetCharacteristicNamesTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetCharacteristicNamesTotalCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.CharacteristicsNames.CountAsync(cancellationToken);
    }
}