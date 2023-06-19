using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Models;

public sealed record GetAllModelsQuery : IRequest<IEnumerable<Model>>;


public sealed class GetAllModelsQueryHandler : IRequestHandler<GetAllModelsQuery, IEnumerable<Model>>
{
    private readonly ApplicationDbContext _context;

    public GetAllModelsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Model>> Handle(GetAllModelsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Models.ToListAsync(cancellationToken);
    }
}