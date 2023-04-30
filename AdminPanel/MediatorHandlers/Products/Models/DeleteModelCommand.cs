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
        var model = await _context.Models.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (model is null) throw new HttpRequestException($"No model with id {request.Id} was found");
        var productWithModel = await _context.Products.FirstOrDefaultAsync(x => x.ModelId == model.Id, cancellationToken);
        if (productWithModel is not null) throw new HttpRequestException($"There are products with this model. Can't delete model");
        _context.Models.Remove(model);
        await _context.SaveChangesAsync(cancellationToken);
    }
}