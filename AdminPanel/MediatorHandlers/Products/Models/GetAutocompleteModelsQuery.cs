using AdminPanel.Data;
using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products.Models
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


    public class GetAutocompleteModelsQueryHandler : IRequestHandler<GetAutocompleteModelsQuery, IQueryable<Model>>
    {
        private readonly ApplicationDbContext _context;

        public GetAutocompleteModelsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Model>> Handle(GetAutocompleteModelsQuery request, CancellationToken cancellationToken)
        {
            return _context.Models.Where(x => x.Name.Contains(request.Input)).Take(request.Take);
        }
    }
}
