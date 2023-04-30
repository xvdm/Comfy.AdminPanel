using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Models;

public record UpdateModelCommand(int Id, string Name) : IRequest;


public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateModelCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        var modelWithName = await _context.Models.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        if (modelWithName?.Id != request.Id)
        {
            if (modelWithName is not null) throw new HttpRequestException($"Model with name {request.Name} already exists");
            var model = await _context.Models.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (model is null) throw new HttpRequestException($"Model with id {request.Id} was not found");
            model.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}