using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record EditSubcategoryCommand(int Id, string Name) : IRequest;


public class EditSubcategoryCommandHandler : IRequestHandler<EditSubcategoryCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public EditSubcategoryCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(EditSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryWithNameCount = await _context.Subcategories.CountAsync(x => x.Name == request.Name, cancellationToken);
        if (categoryWithNameCount > 0) throw new HttpRequestException($"Subcategory with name {request.Name} already exists");

        var category = await _context.Subcategories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category is null) throw new HttpRequestException($"No Subcategory with id {request.Id}");

        category.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}