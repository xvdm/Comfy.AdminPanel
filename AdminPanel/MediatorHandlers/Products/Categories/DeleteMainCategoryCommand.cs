using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record DeleteMainCategoryCommand(int Id) : IRequest<bool>;


public class DeleteMainCategoryCommandHandler : IRequestHandler<DeleteMainCategoryCommand, bool>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public DeleteMainCategoryCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<bool> Handle(DeleteMainCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.MainCategories
            .Include(x => x.Categories)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category is null) throw new HttpRequestException($"No MainCategory with id {request.Id}");

        if (category.Categories.Count > 0) return false; // it's not allowed to delete a main category while there are subcategories in it

        _context.MainCategories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);

        return true;
    }
}