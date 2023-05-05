using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Categories;

public record AddSubcategoryFilterCommand(int SubcategoryId, string SubcategoryFilterName, string SubcategoryFilter) : IRequest;


public class AddSubcategoryFilterCommandHandler : IRequestHandler<AddSubcategoryFilterCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public AddSubcategoryFilterCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(AddSubcategoryFilterCommand request, CancellationToken cancellationToken)
    {
        var subcategory = await _context.Subcategories
            .Include(x => x.Filters)
            .FirstOrDefaultAsync(x => x.Id == request.SubcategoryId, cancellationToken);
        if (subcategory == null) throw new HttpRequestException("No category with given id");

        var filter = new SubcategoryFilter()
        {
            SubcategoryId = subcategory.Id,
            FilterQuery = request.SubcategoryFilter,
            Name = request.SubcategoryFilterName
        };

        if (subcategory.Filters.FirstOrDefault(x => x.Name == request.SubcategoryFilterName) != null) return;
        
        subcategory.Filters.Add(filter);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}