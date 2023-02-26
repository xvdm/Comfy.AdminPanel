using AdminPanel.Commands.Products.Categories;
using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace AdminPanel.Handlers.Products.Categories
{
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
