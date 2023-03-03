using AdminPanel.Data;
using AdminPanel.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string Brand { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Model { get; set; } = null!;
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

            var brand = await _context.Brands.Where(x => x.Name == request.Brand).FirstOrDefaultAsync();
            if(brand is null) throw new HttpRequestException($"There is no brand {request.Brand}");

            var model = await _context.Models.Where(x => x.Name == request.Model).FirstOrDefaultAsync();
            if(model is null) throw new HttpRequestException($"There is no model {request.Brand}");

            var category = await _context.Subcategories.Where(x => x.Name == request.Category).FirstOrDefaultAsync();
            if(category is null) throw new HttpRequestException($"There is no category {request.Brand}");
            
            product.BrandId = brand.Id;
            product.Brand = brand;

            product.Model = model;
            product.ModelId = model.Id;

            product.CategoryId = category.Id;
            product.Category = category;

            product.IsActive = false;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // после получения id товара инициализируется артикул и история цен
            product.Code = product.Id + 1000000;

            product.PriceHistory = new List<PriceHistory>();
            var priceHistory = new PriceHistory
            {
                Date = DateTime.Now,
                Price = product.Price,
                ProductId = product.Id
            };
            product.PriceHistory.Add(priceHistory);

            product.Url = ProductUrl.Create(product.Name, product.Code);

            await _context.SaveChangesAsync();

            return product.Id;
        }
    }
}
