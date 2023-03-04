using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.MediatorHandlers.Products.Brands
{
    public class GetBrandsQuery : IRequest<IEnumerable<Brand>>
    {
        public int CategoryId { get; set; }
        public GetBrandsQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }

    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, IEnumerable<Brand>>
    {
        private readonly ApplicationDbContext _context;

        public GetBrandsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Subcategories
                .Include(x => x.UniqueBrands)
                .FirstOrDefaultAsync(x => x.Id == request.CategoryId);

            if (category is null) throw new HttpRequestException("There is no category with given id");

            var brands = new List<Brand>();
            if(category.UniqueBrands is not null)
            {
                brands = category.UniqueBrands.ToList();
            }

            return brands;
        }
    }
}
