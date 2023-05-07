using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record DeleteSubcategoryCommand(int Id) : IRequest<bool>;


public class DeleteSubcategoryCommandHandler : IRequestHandler<DeleteSubcategoryCommand, bool>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public DeleteSubcategoryCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<bool> Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var productWithCategoryCount = await _context.Products.CountAsync(x => x.CategoryId == request.Id, cancellationToken);
        if (productWithCategoryCount > 0) return false; // it's not allowed to delete a category while there are products in it

        var category = await _context.Subcategories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category is null) throw new HttpRequestException($"No Subcategory with id {request.Id}");

        _context.Subcategories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);

        return true;
    }
}