using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Categories;

public sealed record CreateSubcategoryFilterCommand(int SubcategoryId, string SubcategoryFilterName, string SubcategoryFilter) : IRequest;


public sealed class CreateSubcategoryFilterCommandHandler : IRequestHandler<CreateSubcategoryFilterCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public CreateSubcategoryFilterCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(CreateSubcategoryFilterCommand request, CancellationToken cancellationToken)
    {
        var subcategoryCount = await _context.Subcategories.CountAsync(x => x.Id == request.SubcategoryId, cancellationToken);
        if (subcategoryCount <= 0) throw new HttpRequestException("No category with given id");

        var filterWithNameCount = await _context.SubcategoryFilters
            .CountAsync(x => x.Name == request.SubcategoryFilterName && x.SubcategoryId == request.SubcategoryId, cancellationToken);
        if (filterWithNameCount > 0) return;

        var filter = new SubcategoryFilter()
        {
            SubcategoryId = request.SubcategoryId,
            FilterQuery = request.SubcategoryFilter,
            Name = request.SubcategoryFilterName
        };

        _context.SubcategoryFilters.Add(filter);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}