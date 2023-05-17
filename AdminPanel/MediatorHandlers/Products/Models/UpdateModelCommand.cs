using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Models;

public sealed record UpdateModelCommand(int Id, string Name) : IRequest;


public sealed class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateModelCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        var modelWithNameCount = await _context.Models.CountAsync(x => x.Id != request.Id && x.Name == request.Name, cancellationToken);
        if (modelWithNameCount > 0) throw new HttpRequestException($"Model with name {request.Name} already exists");

        var model = await _context.Models.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (model is null) throw new HttpRequestException($"Model with id {request.Id} was not found");

        model.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}