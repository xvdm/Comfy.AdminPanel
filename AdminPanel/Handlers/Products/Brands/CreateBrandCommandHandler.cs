using AdminPanel.Commands.Brands;
using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using WebApplication2.Models;

namespace AdminPanel.Handlers.Brands
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateBrandCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _context.Brands.AddAsync(request.Brand);
            await _context.SaveChangesAsync();
        }
    }
}
