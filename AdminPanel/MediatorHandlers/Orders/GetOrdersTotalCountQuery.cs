using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Orders;

public sealed record GetOrdersTotalCountQuery(OrderStatusEnum? OrderStatusEnum) : IRequest<int>;


public sealed class GetOrdersTotalCountQueryHandler : IRequestHandler<GetOrdersTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetOrdersTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetOrdersTotalCountQuery request, CancellationToken cancellationToken)
    {
        var count = 0;
        if (request.OrderStatusEnum is null)
        {
            count = await _context.Orders.CountAsync(cancellationToken);
        }
        else
        {
            var orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(x => x.Name == request.OrderStatusEnum.ToString(), cancellationToken);
            if (orderStatus is null) throw new HttpRequestException();
            count = await _context.Orders.CountAsync(x => x.OrderStatusId == orderStatus.Id, cancellationToken);
        }
        return count;
    }
} 