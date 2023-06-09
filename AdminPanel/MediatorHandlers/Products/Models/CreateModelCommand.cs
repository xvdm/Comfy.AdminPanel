using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Models;

public sealed record CreateModelCommand(Model Model) : IRequest;


public sealed class CreateModelCommandHandler : IRequestHandler<CreateModelCommand>
{
    private readonly ApplicationDbContext _context;

    public CreateModelCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var modelWithNameCount = await _context.Models.CountAsync(x => x.Name == request.Model.Name, cancellationToken);
        if (modelWithNameCount > 0) throw new HttpRequestException($"Model with name {request.Model.Name} already exists");

        _context.Models.Add(request.Model);
        await _context.SaveChangesAsync(cancellationToken);
    }
}