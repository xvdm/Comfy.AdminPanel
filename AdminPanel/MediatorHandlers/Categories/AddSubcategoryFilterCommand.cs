using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Categories
{
    public class AddSubcategoryFilterCommand : IRequest
    {
        public int SubcategoryId { get; set; }
        public string SubcategoryFilterName { get; set; }
        public string SubcategoryFilter { get; set; }

        public AddSubcategoryFilterCommand(int subcategoryId, string subcategoryFilterName, string subcategoryFilter)
        {
            SubcategoryId = subcategoryId;
            SubcategoryFilterName = subcategoryFilterName;
            SubcategoryFilter = subcategoryFilter;
        }
    }

    public class AddSubcategoryFilterCommandHandler : IRequestHandler<AddSubcategoryFilterCommand>
    {
        private readonly ApplicationDbContext _context;

        public AddSubcategoryFilterCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AddSubcategoryFilterCommand request, CancellationToken cancellationToken)
        {
            var subcategory = await _context.Subcategories
                .Include(x => x.Filters)
                .FirstOrDefaultAsync(x => x.Id == request.SubcategoryId, cancellationToken);
            if (subcategory == null) throw new HttpRequestException("No category with given id");
            var filter = new SubcategoryFilter()
            {
                SubcategoryId = subcategory.Id,
                Subcategory = subcategory,
                FilterQuery = request.SubcategoryFilter,
                Name = request.SubcategoryFilterName
            };
            if (subcategory.Filters.FirstOrDefault(x => x.Name == request.SubcategoryFilterName) == null)
            {
                subcategory.Filters.Add(filter);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
