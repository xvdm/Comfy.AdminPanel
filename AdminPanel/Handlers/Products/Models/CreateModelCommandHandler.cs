using AdminPanel.Commands.Products.Models;
using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace AdminPanel.Handlers.Products.Models
{
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
