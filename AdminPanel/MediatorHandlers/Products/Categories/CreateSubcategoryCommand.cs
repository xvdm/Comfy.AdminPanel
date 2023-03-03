using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products.Categories
{
    public class CreateSubcategoryCommand : IRequest
    {
        public Subcategory Category { get; set; } = null!;
        public CreateSubcategoryCommand(Subcategory category)
        {
            Category = category;
        }
    }


    public class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateSubcategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var mainCategory = await _context.MainCategories.FirstOrDefaultAsync(x => x.Id == request.Category.MainCategoryId);
            if (mainCategory is null)
            {
                return;
            }
            await _context.Subcategories.AddAsync(request.Category);
            await _context.SaveChangesAsync();
        }
    }
}
