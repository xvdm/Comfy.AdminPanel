using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Orders
{
    public class GetOrdersQuery : IRequest<IEnumerable<Order>?>
    {
        public string OrderStatus { get; set; } = null!;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > _maxPageSize || value < 0) ? _maxPageSize : value;
            }
        }
        public int PageNumber
        {
            get { return _pageNumber; }
            set
            {
                _pageNumber = (value < 1) ? 1 : value;
            }
        }

        private const int _maxPageSize = 15;
        private int _pageSize = _maxPageSize;
        private int _pageNumber = 1;

        public GetOrdersQuery(string orderStatus, int? pageSize, int? pageNumber)
        {
            OrderStatus = orderStatus;
            if (pageSize is not null) PageSize = (int)pageSize;
            if (pageNumber is not null) PageNumber = (int)pageNumber;
        }
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Order>?>
    { 
        private readonly ApplicationDbContext _context;

        public GetOrdersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>?> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var status = await _context.OrderStatuses.FirstOrDefaultAsync(x => x.Status == request.OrderStatus, cancellationToken);
            if (status is null) return null!;

            return await _context.Orders
                .Where(x => x.StatusId == status.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
