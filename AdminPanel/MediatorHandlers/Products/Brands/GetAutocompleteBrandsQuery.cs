using AdminPanel.Data;
using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products.Brands
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


    public class GetAutocompleteBrandsQueryHandler : IRequestHandler<GetAutocompleteBrandsQuery, IQueryable<Brand>>
    {
        private readonly ApplicationDbContext _context;

        public GetAutocompleteBrandsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Brand>> Handle(GetAutocompleteBrandsQuery request, CancellationToken cancellationToken)
        {
            return _context.Brands.Where(x => x.Name.Contains(request.Input)).Take(request.Take);
        }
    }
}
