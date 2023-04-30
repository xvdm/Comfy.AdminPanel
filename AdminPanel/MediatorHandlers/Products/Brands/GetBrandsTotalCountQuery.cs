using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Brands;

public record GetBrandsTotalCountQuery : IRequest<int>;


public class GetBrandsTotalCountQueryHandler : IRequestHandler<GetBrandsTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetBrandsTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetBrandsTotalCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.Brands.CountAsync(cancellationToken);
    }
}