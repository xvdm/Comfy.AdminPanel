using AdminPanel.Data;
using AdminPanel.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products
{
    public class EditProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string Brand { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DiscountAmount { get; set; }
    }


    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public EditProductCommandHandler(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
        }

        public async Task<int> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.Model)
                .Include(x => x.PriceHistory)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (product is null) throw new HttpRequestException("Product was not found");

            if (product.Brand.Name != request.Brand)
            {
                var br = await _context.Brands.FirstOrDefaultAsync(x => x.Name == request.Brand);
                if (br is null) throw new HttpRequestException("This brand does not exist");
            }
            if (product.Category.Name != request.Category)
            {
                var br = await _context.Subcategories.FirstOrDefaultAsync(x => x.Name == request.Category);
                if (br is null) throw new HttpRequestException("This category does not exist");
            }
            if (product.Model.Name != request.Model)
            {
                var br = await _context.Models.FirstOrDefaultAsync(x => x.Name == request.Model);
                if (br is null) throw new HttpRequestException("This model does not exist");
            }


            if (product.Name != request.Name)
            {
                product.Name = request.Name;
                product.Url = ProductUrl.Create(product.Name, product.Code);
            }

            if (product.Price != request.Price)
            {
                product.Price = request.Price;
                var priceHistory = new PriceHistory
                {
                    Date = DateTime.Now,
                    Price = product.Price,
                    ProductId = product.Id
                };
                product.PriceHistory?.Add(priceHistory);
            }

            product.Description = request.Description;
            product.DiscountAmmount = request.DiscountAmount;

            await _context.SaveChangesAsync();

            return product.Id;
        }
    }
}
