using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Brands;

public sealed record DeleteBrandCommand(int Id) : IRequest;


public sealed class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteBrandCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brandtWithModelCount = await _context.Products.CountAsync(x => x.BrandId == request.Id, cancellationToken);
        if (brandtWithModelCount  > 0) throw new HttpRequestException("There are products with this brand. Can't delete brand");

        var brand = await _context.Brands
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (brand is null) return;

        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync(cancellationToken);
    }
}