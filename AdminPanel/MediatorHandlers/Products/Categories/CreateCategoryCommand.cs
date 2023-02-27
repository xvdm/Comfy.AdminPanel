using AdminPanel.Data;
using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products.Categories
{
    public class CreateCategoryCommand : IRequest
    {
        public Category Category { get; set; } = null!;
        public CreateCategoryCommand(Category category)
        {
            Category = category;
        }
    }


    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateCategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(request.Category);
            await _context.SaveChangesAsync();
        }
    }
}
