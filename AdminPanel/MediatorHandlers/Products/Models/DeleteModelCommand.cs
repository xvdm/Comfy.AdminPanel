using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Models;

public record DeleteModelCommand(int Id) : IRequest;


public class DeleteModelCommandHandler : IRequestHandler<DeleteModelCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteModelCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteModelCommand request, CancellationToken cancellationToken)
    {
        var productWithModelCount = await _context.Products.CountAsync(x => x.ModelId == request.Id, cancellationToken);
        if (productWithModelCount > 0) throw new HttpRequestException("There are products with this model. Can't delete model");

        var model = await _context.Models
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (model is null) throw new HttpRequestException($"No model with id {request.Id} was found");

        _context.Models.Remove(model);
        await _context.SaveChangesAsync(cancellationToken);
    }
}