using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories
{
    public class DeleteMainCategoryCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteMainCategoryCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteMainCategoryCommandHandler : IRequestHandler<DeleteMainCategoryCommand, bool>
    {
        private ApplicationDbContext _context;

        public DeleteMainCategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteMainCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.MainCategories
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (category is null) throw new HttpRequestException($"No MainCategory with id {request.Id}");

            if (category.Categories.Count > 0) return false; // нельзя удалять главную категорию, пока в ней есть подкатегории

            _context.MainCategories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
