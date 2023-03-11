using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Models;

namespace AdminPanel.MediatorHandlers.Products.Models
{
    public class GetModelsQuery : IRequest<IEnumerable<Model>>
    {
    }

    public class GetBrandsQueryHandler : IRequestHandler<GetModelsQuery, IEnumerable<Model>>
    {
        private readonly ApplicationDbContext _context;

        public GetBrandsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Model>> Handle(GetModelsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Models.ToListAsync();
        }
    }
}
