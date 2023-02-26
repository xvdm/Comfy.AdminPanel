using MediatR;

namespace AdminPanel.Commands.Products
{
    public class AddProductCharacteristicCommand : IRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
