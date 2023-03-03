using AdminPanel.Data;
using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products.Categories
{
    public class GetAutocompleteSubcategoriesQuery : IRequest<IQueryable<Subcategory>>
    {
        public string Input { get; set; }
        public int Take { get; set; }
        public GetAutocompleteSubcategoriesQuery(string input, int take)
        {
            Input = input;
            Take = take;
        }
    }


    public class GetAutocompleteCategoriesQueryHandler : IRequestHandler<GetAutocompleteSubcategoriesQuery, IQueryable<Subcategory>>
    {
        private readonly ApplicationDbContext _context;

        public GetAutocompleteCategoriesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Subcategory>> Handle(GetAutocompleteSubcategoriesQuery request, CancellationToken cancellationToken)
        {
            return _context.Subcategories.Where(x => x.Name.Contains(request.Input)).Take(request.Take);
        }
    }
}
