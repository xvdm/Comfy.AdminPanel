using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.CharacteristicGroups;

public sealed record AddCharacteristicGroupCommand(int ProductId, string Name) : IRequest;


public sealed class AddCharacteristicGroupCommandHandler : IRequestHandler<AddCharacteristicGroupCommand>
{
    private readonly ApplicationDbContext _context;

    public AddCharacteristicGroupCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddCharacteristicGroupCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(x => x.CharacteristicGroups)
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null) return;
        if (product.CharacteristicGroups.Any(characteristicGroup => characteristicGroup.Name == request.Name.Trim()))
        {
            throw new Exception("Група з такою назвою вже є");
        }

        var group = new CharacteristicGroup
        {
            Name = request.Name.Trim(),
            ProductId = request.ProductId
        };

        _context.CharacteristicGroups.Add(group);
        await _context.SaveChangesAsync(cancellationToken);
    }
}