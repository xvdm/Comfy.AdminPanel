using AdminPanel.Data;
using AdminPanel.Helpers;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public record CreateProductCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public int Price { get; set; }
    public int BrandId { get; set; }
    public int SubcategoryId { get; set; }
    public int ModelId { get; set; }
    public string Description { get; set; } = null!;
}


public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateProductCommandHandler(ApplicationDbContext context, IMediator mediator)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            BrandId = request.BrandId,
            ModelId = request.ModelId,
            CategoryId = request.SubcategoryId,
            Description = request.Description,
            IsActive = false,
            Url = "",
            Amount = 0
        };

        var modelCount = await _context.Models.CountAsync(x => x.Id == request.ModelId, cancellationToken);
        if (modelCount <= 0) throw new HttpRequestException($"There is no model {request.ModelId}");

        var brand = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.BrandId, cancellationToken);
        if(brand is null) throw new HttpRequestException($"There is no brand {request.BrandId}");

        var category = await _context.Subcategories.Include(x => x.UniqueBrands).FirstOrDefaultAsync(x => x.Id == request.SubcategoryId, cancellationToken);
        if (category is null) throw new HttpRequestException($"There is no category {request.SubcategoryId}");

        category.UniqueBrands.Add(brand);

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        product.Code = product.Id + 1000000;
        product.Url = ProductUrl.Create(product.Name, product.Code);
        product.PriceHistory = new List<PriceHistory>
        {
            new()
            {
                Date = DateTime.Now,
                Price = product.Price,
                ProductId = product.Id
            }
        };

        await _context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}