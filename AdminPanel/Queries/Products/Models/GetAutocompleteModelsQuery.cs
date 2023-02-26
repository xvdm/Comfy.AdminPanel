using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Queries.Products.Models
{
    public class GetAutocompleteModelsQuery : IRequest<IQueryable<Model>>
    {
        public string Input { get; set; }
        public int Take { get; set; }

        public GetAutocompleteModelsQuery(string input, int take)
        {
            Input = input;
            Take = take;
        }
    }
}
