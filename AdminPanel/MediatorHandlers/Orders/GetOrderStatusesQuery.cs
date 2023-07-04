using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Orders;

public sealed record GetOrderStatusesQuery : IRequest<List<OrderStatus>>;


public sealed class GetOrderStatusesQueryHandler : IRequestHandler<GetOrderStatusesQuery, List<OrderStatus>>
{
    private readonly ApplicationDbContext _context;

    public GetOrderStatusesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderStatus>> Handle(GetOrderStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _context.OrderStatuses.ToListAsync(cancellationToken);
    }
}