using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Queries.Products.Categories
{
    public class GetAutocompleteCategoriesQuery : IRequest<IQueryable<Category>>
    {
        public string Input { get; set; }
        public int Take { get; set; }

        public GetAutocompleteCategoriesQuery(string input, int take)
        {
            Input = input;
            Take = take;
        }
    }
}
