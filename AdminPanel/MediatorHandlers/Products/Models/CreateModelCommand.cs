﻿using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Models;

public record CreateModelCommand(Model Model) : IRequest<Model>;


public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, Model>
{
    private readonly ApplicationDbContext _context;

    public CreateModelCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Model> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var modelWithName = await _context.Models.FirstOrDefaultAsync(x => x.Name == request.Model.Name, cancellationToken);
        if (modelWithName is not null) throw new HttpRequestException($"Model with name {request.Model.Name} already exists");
        await _context.Models.AddAsync(request.Model, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return request.Model;
    }
}