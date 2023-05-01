using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Categories;

public record DeleteSubcategoryFilterCommand(int SubcategoryFilterId) : IRequest;


public class DeleteSubcategoryFilterCommandHandler : IRequestHandler<DeleteSubcategoryFilterCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public DeleteSubcategoryFilterCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(DeleteSubcategoryFilterCommand request, CancellationToken cancellationToken)
    {
        var filter = await _context.SubcategoryFilters.FirstOrDefaultAsync(x => x.Id == request.SubcategoryFilterId, cancellationToken);
        if(filter == null) return;
        _context.SubcategoryFilters.Remove(filter);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}