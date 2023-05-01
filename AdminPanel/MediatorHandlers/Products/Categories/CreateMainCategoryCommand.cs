using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models;
using MediatR;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record CreateMainCategoryCommand(MainCategory Category) : IRequest;


public class CreateMainCategoryCommandHandler : IRequestHandler<CreateMainCategoryCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public CreateMainCategoryCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(CreateMainCategoryCommand request, CancellationToken cancellationToken)
    {
        await _context.MainCategories.AddAsync(request.Category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}