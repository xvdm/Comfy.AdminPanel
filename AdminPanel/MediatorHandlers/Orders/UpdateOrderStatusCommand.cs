using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Orders;

public sealed record UpdateOrderStatusCommand(int Id, OrderStatusEnum NewStatus) : IRequest;


public sealed class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateOrderStatusCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var status = await _context.OrderStatuses.FirstOrDefaultAsync(x => x.Id == (int)request.NewStatus, cancellationToken);
        if (status is null) throw new HttpRequestException();

        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (order is null) throw new HttpRequestException();

        order.OrderStatusId = status.Id;
        await _context.SaveChangesAsync(cancellationToken);
    }
}