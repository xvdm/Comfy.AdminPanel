using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record CreateMainCategoryCommand(MainCategory Category) : IRequest;


public class CreateMainCategoryCommandHandler : IRequestHandler<CreateMainCategoryCommand>
{
    private readonly ApplicationDbContext _context;

    public CreateMainCategoryCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateMainCategoryCommand request, CancellationToken cancellationToken)
    {
        await _context.MainCategories.AddAsync(request.Category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}