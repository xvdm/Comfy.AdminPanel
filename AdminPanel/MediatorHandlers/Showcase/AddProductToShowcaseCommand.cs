using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase;

public record AddProductToShowcaseCommand(int GroupId, int ProductCode) : IRequest;


public class AddProductToShowcaseCommandHandler : IRequestHandler<AddProductToShowcaseCommand>
{
    private readonly ApplicationDbContext _context;
    private const int MaxProductsInGroup = 4;

    public AddProductToShowcaseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddProductToShowcaseCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.ShowcaseGroups
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if (group is null || group.Products.Count >= MaxProductsInGroup) return;

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Code == request.ProductCode, cancellationToken);
        if (product is null) return;

        if (group.Products.Contains(product)) return;

        group.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
}