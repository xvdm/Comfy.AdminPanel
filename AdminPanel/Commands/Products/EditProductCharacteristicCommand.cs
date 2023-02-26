using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Commands.Products
{
    public class EditProductCharacteristicCommand : IRequest
    {
        public Product Product { get; set; } = null!;
        public int CharacteristicId { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
