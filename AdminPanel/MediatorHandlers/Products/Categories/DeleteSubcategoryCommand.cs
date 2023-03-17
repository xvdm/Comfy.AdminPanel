using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories
{
    public class DeleteSubcategoryCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteSubcategoryCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteSubcategoryCommandHandler : IRequestHandler<DeleteSubcategoryCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteSubcategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Subcategories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (category is null) throw new HttpRequestException($"No Subcategory with id {request.Id}");

            var productWithCategory = await _context.Products.FirstOrDefaultAsync(x => x.CategoryId == request.Id, cancellationToken);
            if (productWithCategory is not null) return false; // нельзя удалять категорию, в которой есть товары

            _context.Subcategories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
