using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Models;
using System.Xml.Linq;

namespace AdminPanel.Handlers.Products
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        private const int _maxPageSize = 15;
        private int _pageSize = _maxPageSize;
        private int _pageNumber = 1;

        public string? SearchString { get; set; }
        public int? CategoryId { get; set; }

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > _maxPageSize || value < 0) ? _maxPageSize : value;
            }
        }
        public int PageNumber
        {
            get { return _pageNumber; }
            set
            {
                _pageNumber = (value < 1) ? 1 : value;
            }
        }

        public Dictionary<string, List<string>> QueryDictionary { get; set; }

        public GetProductsQuery(string? searchString, int? pageSize, int? pageNumber, int? categoryId, Dictionary<string, List<string>> queryDictionary)
        {
            SearchString = searchString;
            CategoryId = categoryId;
            QueryDictionary = queryDictionary;
            if (pageSize is not null) PageSize = (int)pageSize;
            if (pageNumber is not null) PageNumber = (int)pageNumber;
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

            if (request.SearchString is not null)
            {
                products = products.Where(p => p.Name.Contains(request.SearchString));
            }

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
            return await products
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }
    }
}
