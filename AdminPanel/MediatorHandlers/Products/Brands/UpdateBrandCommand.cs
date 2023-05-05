using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Brands;

public record UpdateBrandCommand(int Id, string Name) : IRequest;


public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateBrandCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brandWithNameCount = await _context.Brands.CountAsync(x => x.Id != request.Id && x.Name == request.Name, cancellationToken);
        if (brandWithNameCount > 0) throw new HttpRequestException($"Brand with name {request.Name} already exists");

        var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (brand is null) throw new HttpRequestException($"Brand with id {request.Id} was not found");

        brand.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}