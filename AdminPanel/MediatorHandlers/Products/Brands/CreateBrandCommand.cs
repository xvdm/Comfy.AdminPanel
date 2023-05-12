using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Brands;

public record CreateBrandCommand(Brand Brand) : IRequest;


public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand>
{
    private readonly ApplicationDbContext _context;

    public CreateBrandCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brandWithNameCount = await _context.Brands.CountAsync(x => x.Name == request.Brand.Name, cancellationToken);
        if (brandWithNameCount > 0) throw new HttpRequestException($"Brand with name {request.Brand.Name} already exists");

        _context.Brands.Add(request.Brand);
        await _context.SaveChangesAsync(cancellationToken);
    }
}