using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Orders;

public sealed record GetOrdersQuery : IRequest<IEnumerable<Order>?>
{
    public string OrderStatus { get; set; }

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

    public GetOrdersQuery(string orderStatus, int? pageSize, int? pageNumber)
    {
        OrderStatus = orderStatus;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}

public sealed class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Order>?>
{ 
    private readonly ApplicationDbContext _context;

    public GetOrdersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>?> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var status = await _context.OrderStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Status == request.OrderStatus, cancellationToken);
        if (status is null) return null!;

        return await _context.Orders
            .Where(x => x.StatusId == status.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}