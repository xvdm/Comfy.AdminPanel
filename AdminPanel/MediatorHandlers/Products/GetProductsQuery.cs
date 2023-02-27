using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public GetProductsQuery(int skip, int take)
        {
            Skip = skip;
            Take = take;
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
            return await _context.Products.Skip(request.Skip).Take(request.Take).ToListAsync();
        }
    }
}
