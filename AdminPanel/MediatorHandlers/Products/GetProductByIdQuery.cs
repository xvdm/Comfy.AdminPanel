using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public record GetProductByIdQuery(int ProductId) : IRequest<Product?>;


public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly ApplicationDbContext _context;

    public GetProductByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(x => x.Category)
                .ThenInclude(x => x.MainCategory)
            .Include(x => x.Characteristics)
                .ThenInclude(x => x.CharacteristicsName)
            .Include(x => x.Characteristics)
                .ThenInclude(x => x.CharacteristicsValue)
            .Include(x => x.Brand)
            .Include(x => x.Category)
            .Include(x => x.Model)
            .Include(x => x.Images)
            .Include(x => x.PriceHistory)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        return product;
    }
}