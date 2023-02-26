using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Commands.Products
{
    public class EditProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string Brand { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DiscountAmount { get; set; }
    }
}
