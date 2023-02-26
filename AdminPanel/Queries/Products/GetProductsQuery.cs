using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Queries.Products
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
}
