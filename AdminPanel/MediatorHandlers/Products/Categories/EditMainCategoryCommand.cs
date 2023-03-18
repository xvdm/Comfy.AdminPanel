using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories
{
    public class EditMainCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EditMainCategoryCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class EditMainCategoryCommandHandler : IRequestHandler<EditMainCategoryCommand>
    {
        private readonly ApplicationDbContext _context;

        public EditMainCategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(EditMainCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.MainCategories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (category is null) throw new HttpRequestException($"No MainCategory with id {request.Id}");
            var categoryWithName = await _context.MainCategories.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (categoryWithName is not null) throw new HttpRequestException($"Main Category with name {request.Name} already exists");
            category.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
