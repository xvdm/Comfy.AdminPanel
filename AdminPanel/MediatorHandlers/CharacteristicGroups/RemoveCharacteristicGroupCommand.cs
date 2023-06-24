using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.CharacteristicGroups;
public sealed record RemoveCharacteristicGroupCommand(int GroupId) : IRequest;


public sealed class RemoveCharacteristicGroupCommandHandler : IRequestHandler<RemoveCharacteristicGroupCommand>
{
    private readonly ApplicationDbContext _context;

    public RemoveCharacteristicGroupCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveCharacteristicGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.CharacteristicGroups
            .Include(x => x.Characteristics)
            .FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if (group is null) return;
        if (group.Characteristics.Any()) throw new Exception("Не можна видаляти групу, в котрій є характеристики");
        _context.CharacteristicGroups.Remove(group);
        await _context.SaveChangesAsync(cancellationToken);
    }
}