using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Brands;

public sealed record GetAllBrandsQuery : IRequest<IEnumerable<Brand>>;


public sealed class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<Brand>>
{
    private readonly ApplicationDbContext _context;

    public GetAllBrandsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Brand>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Brands.ToListAsync(cancellationToken);
    }
}