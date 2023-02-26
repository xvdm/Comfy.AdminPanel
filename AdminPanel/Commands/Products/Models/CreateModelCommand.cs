using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Commands.Products.Models
{
    public class CreateModelCommand : IRequest
    {
        public Model Model { get; set; } = null!; 

        public CreateModelCommand(Model model)
        {
            Model = model;
        }
    }
}
