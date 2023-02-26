using MediatR;

namespace AdminPanel.Commands.Products
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
}
