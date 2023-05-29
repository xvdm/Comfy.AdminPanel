using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public sealed record GetProductsTotalCountQuery : IRequest<int>;


public sealed class GetProductsTotalCountQueryHandler : IRequestHandler<GetProductsTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetProductsTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetProductsTotalCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products.CountAsync(cancellationToken);
    }
} 