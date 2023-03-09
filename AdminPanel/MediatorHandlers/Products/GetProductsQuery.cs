using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public int? CategoryId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public Dictionary<string, List<string>> QueryDictionary { get; set; }
        public GetProductsQuery(int? categoryId, int skip, int take, Dictionary<string, List<string>> queryDictionary)
        {
            CategoryId = categoryId;
            Skip = skip;
            Take = take;
            QueryDictionary = queryDictionary;
        }
    }


    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly ApplicationDbContext _context;

        public GetProductsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _context.Products
                .AsNoTracking()
                .AsQueryable();

            if (request.CategoryId is not null)
            {
                products = products
                    .Where(x => x.CategoryId == request.CategoryId);
            }

            products = products
                    .Include(p => p.Model)
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Include(p => p.Characteristics);


            foreach (var pair in request.QueryDictionary)
            {
                if (pair.Value.Any())
                {
                    var ids = pair.Value.Where(x => int.TryParse(x, out int id)).Select(x => int.Parse(x)).ToArray();

                    if (pair.Key == "brand")
                    {
                        products = products.Where(x => ids.Contains(x.Brand.Id));
                    }
                    else
                    {
                        products = products.Where(x => x.Characteristics.Any(c => ids.Contains(c.CharacteristicsValueId)));                    
                    }
                }
            }
            return await products.Skip(request.Skip).Take(request.Take).ToListAsync();
        }
    }
}
