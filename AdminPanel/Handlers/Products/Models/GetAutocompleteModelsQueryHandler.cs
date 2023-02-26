using AdminPanel.Data;
using AdminPanel.Queries.Products.Categories;
using AdminPanel.Queries.Products.Models;
using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products.Models
{
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
