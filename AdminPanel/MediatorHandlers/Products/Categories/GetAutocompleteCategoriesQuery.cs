using AdminPanel.Data;
using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products.Categories
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


    public class GetAutocompleteCategoriesQueryHandler : IRequestHandler<GetAutocompleteCategoriesQuery, IQueryable<Category>>
    {
        private readonly ApplicationDbContext _context;

        public GetAutocompleteCategoriesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Category>> Handle(GetAutocompleteCategoriesQuery request, CancellationToken cancellationToken)
        {
            return _context.Categories.Where(x => x.Name.Contains(request.Input)).Take(request.Take);
        }
    }
}
