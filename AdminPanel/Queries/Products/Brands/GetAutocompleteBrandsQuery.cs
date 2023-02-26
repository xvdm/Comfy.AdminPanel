using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Queries.Products.Brands
{
    public class GetAutocompleteBrandsQuery : IRequest<IQueryable<Brand>>
    {
        public string Input { get; set; }
        public int Take { get; set; }

        public GetAutocompleteBrandsQuery(string input, int take)
        {
            Input = input;
            Take = take;
        }
    }
}
