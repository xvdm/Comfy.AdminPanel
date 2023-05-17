using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase;

public sealed record DeleteProductFromShowcaseCommand(int GroupId, int ProductId) : IRequest;


public sealed class DeleteProductFromShowcaseCommandHandler : IRequestHandler<DeleteProductFromShowcaseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public DeleteProductFromShowcaseCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(DeleteProductFromShowcaseCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.ShowcaseGroups
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if (group is null) return;

        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null) return;

        group.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new ShowcaseGroupsInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}