using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record EditSubcategoryCommand(int Id, string Name) : IRequest;


public class EditSubcategoryCommandHandler : IRequestHandler<EditSubcategoryCommand>
{
    private readonly ApplicationDbContext _context;

    public EditSubcategoryCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Subcategories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category is null) throw new HttpRequestException($"No Subcategory with id {request.Id}");
        var categoryWithName = await _context.Subcategories.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        if (categoryWithName is not null) throw new HttpRequestException($"Subcategory with name {request.Name} already exists");
        category.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}