using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase
{
    public class AddProductToShowcaseCommand : IRequest
    {
        public int GroupId { get; set; }
        public int ProductCode { get; set; }

        public AddProductToShowcaseCommand(int groupId, int productCode)
        {
            GroupId = groupId;
            ProductCode = productCode;
        }
    }

    public class AddProductToShowcaseCommandHandler : IRequestHandler<AddProductToShowcaseCommand>
    {
        private readonly ApplicationDbContext _context;

        public AddProductToShowcaseCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AddProductToShowcaseCommand request, CancellationToken cancellationToken)
        {
            var group = await _context.ShowcaseGroups
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
            if (group is null || group.Products.Count >= 4) return;

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Code == request.ProductCode, cancellationToken);
            if (product is null) return;

            if (group.Products.Contains(product)) return;

            group.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
