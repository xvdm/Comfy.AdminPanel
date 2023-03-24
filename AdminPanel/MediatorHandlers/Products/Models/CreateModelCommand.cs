using AdminPanel.Data;
using MediatR;
using AdminPanel.Models;

namespace AdminPanel.Handlers.Products.Models
{
    public class CreateModelCommand : IRequest<Model>
    {
        public Model Model { get; set; }
        public CreateModelCommand(Model model)
        {
            Model = model;
        }
    }


    public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, Model>
    {
        private readonly ApplicationDbContext _context;

        public CreateModelCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Model> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            await _context.Models.AddAsync(request.Model, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Model;
        }
    }
}
