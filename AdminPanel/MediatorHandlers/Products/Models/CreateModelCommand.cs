using AdminPanel.Data;
using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Products.Models
{
    public class CreateModelCommand : IRequest
    {
        public Model Model { get; set; } = null!;
        public CreateModelCommand(Model model)
        {
            Model = model;
        }
    }


    public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateModelCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            await _context.Models.AddAsync(request.Model);
            await _context.SaveChangesAsync();
        }
    }
}
