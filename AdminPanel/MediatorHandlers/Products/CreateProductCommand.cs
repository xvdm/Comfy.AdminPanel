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
    public int Brand { get; set; }
    public int Category { get; set; }
    public int Model { get; set; }
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
            Price = request.Price
        };

        var brand = await _context.Brands
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Brand, cancellationToken);
        if(brand is null) throw new HttpRequestException($"There is no brand {request.Brand}");

        var model = await _context.Models
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Model, cancellationToken);
        if(model is null) throw new HttpRequestException($"There is no model {request.Brand}");

        var category = await _context.Subcategories
            .Include(x => x.UniqueBrands)
            .FirstOrDefaultAsync(x => x.Id == request.Category, cancellationToken);
        if(category is null) throw new HttpRequestException($"There is no category {request.Brand}");


        product.BrandId = brand.Id;
        product.ModelId = model.Id;
        product.CategoryId = category.Id;
        product.Description = request.Description;
        product.IsActive = false;
        product.Url = "";

        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        // price history is being initialized after getting product id 
        product.Code = product.Id + 1000000;
        
        if(category.UniqueBrands.Contains(product.Brand) == false) category.UniqueBrands.Add(product.Brand);

        product.PriceHistory = new List<PriceHistory>();
        var priceHistory = new PriceHistory
        {
            Date = DateTime.Now,
            Price = product.Price,
            ProductId = product.Id
        };
        product.PriceHistory.Add(priceHistory);

        product.Url = ProductUrl.Create(product.Name, product.Code);

        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}