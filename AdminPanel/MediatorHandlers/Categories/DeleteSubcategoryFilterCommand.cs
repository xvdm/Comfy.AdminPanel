using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Categories;

public record DeleteSubcategoryFilterCommand(int SubcategoryFilterId) : IRequest;


public class DeleteSubcategoryFilterCommandHandler : IRequestHandler<DeleteSubcategoryFilterCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteSubcategoryFilterCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSubcategoryFilterCommand request, CancellationToken cancellationToken)
    {
        var filter = await _context.SubcategoryFilters.FirstOrDefaultAsync(x => x.Id == request.SubcategoryFilterId, cancellationToken);
        if(filter == null) return;
        _context.SubcategoryFilters.Remove(filter);
        await _context.SaveChangesAsync(cancellationToken);
    }
}