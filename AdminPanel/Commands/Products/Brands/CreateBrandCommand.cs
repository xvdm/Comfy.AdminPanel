using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Commands.Brands
{
    public class CreateBrandCommand : IRequest
    {
        public Brand Brand { get; set; } = null!;

        public CreateBrandCommand(Brand brand)
        {
            Brand = brand;
        }
    }
}
