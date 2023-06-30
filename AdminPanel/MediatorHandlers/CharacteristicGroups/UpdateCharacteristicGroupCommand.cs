using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.CharacteristicGroups;

public sealed record UpdateCharacteristicGroupCommand(int GroupId, string Name) : IRequest;


public sealed class UpdateCharacteristicGroupCommandHandler : IRequestHandler<UpdateCharacteristicGroupCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateCharacteristicGroupCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCharacteristicGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.CharacteristicGroups.FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if (group is null) return;
        var product = await _context.Products
            .Include(x => x.CharacteristicGroups)
            .FirstOrDefaultAsync(x => x.Id == group.ProductId, cancellationToken);
        if (product is null) return;
        if (product.CharacteristicGroups.Any(characteristicGroup => characteristicGroup.Name == request.Name.Trim()))
        {
            throw new Exception("Група з такою назвою вже є");
        }
        group.Name = request.Name.Trim();
        await _context.SaveChangesAsync(cancellationToken);
    }
}