using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Models;
using System.Xml.Linq;

namespace AdminPanel.Handlers.Products
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public string? SearchString { get; set; }

        private const int MaxPageSize = 15;
        private int _pageSize = MaxPageSize;
        private int _pageNumber = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value is > MaxPageSize or < 1 ? MaxPageSize : value;
        }
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = (value < 1) ? 1 : value;
        }

        public GetProductsQuery(string? searchString, int? pageSize, int? pageNumber)
        {
            SearchString = searchString;
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

            products = products
                    .Include(p => p.Model)
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Include(p => p.Characteristics);

            return await products
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }
    }
}
