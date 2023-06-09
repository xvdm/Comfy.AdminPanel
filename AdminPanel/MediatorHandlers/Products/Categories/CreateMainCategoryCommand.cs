using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models.Entities;
using MediatR;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public sealed record CreateMainCategoryCommand(MainCategory Category) : IRequest;


public sealed class CreateMainCategoryCommandHandler : IRequestHandler<CreateMainCategoryCommand>
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
        _context.MainCategories.Add(request.Category);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}