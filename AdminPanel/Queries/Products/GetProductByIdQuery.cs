using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Queries.Products
{
    public class GetProductByIdQuery : IRequest<Product?>
    {
        public int ProductId{ get; }
        public GetProductByIdQuery(int id)
        {
            ProductId = id;
        }
    }
}
