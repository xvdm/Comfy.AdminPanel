using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Characteristics;

public sealed record GetCharacteristicValuesTotalCountQuery : IRequest<int>;


public sealed class GetCharacteristicValuesTotalCountQueryHandler : IRequestHandler<GetCharacteristicValuesTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetCharacteristicValuesTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetCharacteristicValuesTotalCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.CharacteristicsValues.CountAsync(cancellationToken);
    }
}