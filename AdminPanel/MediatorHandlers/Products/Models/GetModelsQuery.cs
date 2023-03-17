using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Models;

namespace AdminPanel.MediatorHandlers.Products.Models
{
    public class GetModelsQuery : IRequest<IEnumerable<Model>>
    {
        private const int MaxPageSize = 15;
        private int _pageSize = MaxPageSize;
        private int _pageNumber = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value is > MaxPageSize or < 1 ? MaxPageSize : value;
        }
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = (value < 1) ? 1 : value;
        }

        public GetModelsQuery(int? pageSize, int? pageNumber)
        {
            if (pageSize is not null) PageSize = (int)pageSize;
            if (pageNumber is not null) PageNumber = (int)pageNumber;
        }
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
            var models = await _context.Models
                .AsNoTracking()
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return models;
        }
    }
}
