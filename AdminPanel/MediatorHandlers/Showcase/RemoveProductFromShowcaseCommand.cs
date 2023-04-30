using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase;

public record RemoveProductFromShowcaseCommand(int GroupId, int ProductId) : IRequest;


public class RemoveProductFromShowcaseCommandHandler : IRequestHandler<RemoveProductFromShowcaseCommand>
{
    private readonly ApplicationDbContext _context;

    public RemoveProductFromShowcaseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveProductFromShowcaseCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.ShowcaseGroups
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if (group is null) return;

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null) return;

        group.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
}