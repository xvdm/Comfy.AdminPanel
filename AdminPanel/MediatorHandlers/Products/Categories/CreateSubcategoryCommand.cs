using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Helpers;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public sealed record CreateSubcategoryCommand(Subcategory Category) : IRequest;


public sealed class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public CreateSubcategoryCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var mainCategoryCount = await _context.MainCategories.CountAsync(x => x.Id == request.Category.MainCategoryId, cancellationToken);
        if (mainCategoryCount <= 0) return;

        request.Category.Url = UrlHelper.CreateCategoryUrl(request.Category.Name);
        _context.Subcategories.Add(request.Category);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}