using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public sealed record UpdateSubcategoryCommand(int Id, string Name) : IRequest;


public sealed class UpdateSubcategoryCommandHandler : IRequestHandler<UpdateSubcategoryCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public UpdateSubcategoryCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryWithNameCount = await _context.Subcategories.CountAsync(x => x.Name == request.Name, cancellationToken);
        if (categoryWithNameCount > 0) throw new HttpRequestException($"Subcategory with name {request.Name} already exists");

        var category = await _context.Subcategories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category is null) throw new HttpRequestException($"No Subcategory with id {request.Id}");

        category.Name = request.Name;
        category.Url = UrlHelper.CreateCategoryUrl(request.Name);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}