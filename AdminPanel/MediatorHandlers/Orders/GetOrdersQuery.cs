using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Orders;

public sealed record GetOrdersQuery : IRequest<IEnumerable<Order>>
{
    public OrderStatusEnum? OrderStatusEnum { get; set; }

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

    public GetOrdersQuery(OrderStatusEnum? orderStatusEnum, int? pageSize, int? pageNumber)
    {
        OrderStatusEnum = orderStatusEnum;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}

public sealed class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Order>>
{ 
    private readonly ApplicationDbContext _context;

    public GetOrdersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        if (request.OrderStatusEnum is not null)
        {
            var status = await _context.OrderStatuses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == request.OrderStatusEnum.ToString(), cancellationToken);
            if (status is null) throw new HttpRequestException();
        }

        var ordersQueryable = _context.Orders
            .Include(x => x.OrderStatus)
            .Include(x => x.Products.OrderBy(y => y.Id))
            .AsQueryable();

        if (request.OrderStatusEnum is not null)
        {
            ordersQueryable = ordersQueryable.Where(x => x.OrderStatusId == (int)request.OrderStatusEnum);
        }

        return await ordersQueryable
            .OrderByDescending(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}