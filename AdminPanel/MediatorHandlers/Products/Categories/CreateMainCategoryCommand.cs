using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;

namespace AdminPanel.Handlers.Products.Categories
{
    public class CreateMainCategoryCommand : IRequest
    {
        public MainCategory Category { get; set; } = null!;
        public CreateMainCategoryCommand(MainCategory category)
        {
            Category = category;
        }
    }


    public class CreateMainCategoryCommandHandler : IRequestHandler<CreateMainCategoryCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateMainCategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateMainCategoryCommand request, CancellationToken cancellationToken)
        {
            await _context.MainCategories.AddAsync(request.Category);
            await _context.SaveChangesAsync();
        }
    }
}
