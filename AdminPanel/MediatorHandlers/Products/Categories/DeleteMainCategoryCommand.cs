using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record DeleteMainCategoryCommand(int Id) : IRequest<bool>;


public class DeleteMainCategoryCommandHandler : IRequestHandler<DeleteMainCategoryCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteMainCategoryCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteMainCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.MainCategories
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category is null) throw new HttpRequestException($"No MainCategory with id {request.Id}");

        if (category.Categories.Count > 0) return false; // it's not allowed to delete a main category while there are subcategories in it

        _context.MainCategories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}