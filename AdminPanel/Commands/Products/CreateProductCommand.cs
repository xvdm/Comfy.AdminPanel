using MediatR;

namespace AdminPanel.Commands.Products
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;
        public int Price{ get; set; }
        public string Brand { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Model { get; set; } = null!;
    }
}
