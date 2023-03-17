using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Handlers.Products
{
    public class ChangeProductActivityStatusCommand : IRequest
    {
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
        public ChangeProductActivityStatusCommand(int productId, bool isActive)
        {
            ProductId = productId;
            IsActive = isActive;
        }
    }


    public class ChangeProductActivityStatusCommandHandler : IRequestHandler<ChangeProductActivityStatusCommand>
    {
        private readonly ApplicationDbContext _context;

        public ChangeProductActivityStatusCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(ChangeProductActivityStatusCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
            if (product is null) throw new HttpRequestException("Product was not found");
            product.IsActive = request.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
