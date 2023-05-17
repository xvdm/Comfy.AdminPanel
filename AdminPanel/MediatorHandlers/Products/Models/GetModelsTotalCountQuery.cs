using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Models;

public sealed record GetModelsTotalCountQuery : IRequest<int>;


public sealed class GetModelsTotalCountQueryHandler : IRequestHandler<GetModelsTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetModelsTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetModelsTotalCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.Models.CountAsync(cancellationToken);
    }
}