using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public sealed record GetProductsTotalCountQuery(string? SearchString) : IRequest<int>;


public sealed class GetProductsTotalCountQueryHandler : IRequestHandler<GetProductsTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetProductsTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetProductsTotalCountQuery request, CancellationToken cancellationToken)
    {
        var products = _context.Products.AsQueryable();

        if (string.IsNullOrWhiteSpace(request.SearchString) == false)
        {
            products = products.Where(x => x.Name.Contains(request.SearchString));
        }

        return await products.CountAsync(cancellationToken);
    }
} 