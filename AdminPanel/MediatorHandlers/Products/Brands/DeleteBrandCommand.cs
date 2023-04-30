using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Brands;

public record DeleteBrandCommand(int Id) : IRequest;


public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteBrandCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (brand is null) throw new HttpRequestException($"No brand with id {request.Id} was found");
        var brandtWithModel = await _context.Products.FirstOrDefaultAsync(x => x.BrandId == brand.Id, cancellationToken);
        if (brandtWithModel is not null) throw new HttpRequestException($"There are products with this brand. Can't delete brand");
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync(cancellationToken);
    }
}