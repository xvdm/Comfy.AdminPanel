using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Orders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
[Authorize(Policy = PoliciesNames.Administrator)]
public sealed class OrdersController : Controller
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> ActiveOrders(int? pageSize, int? pageNumber)
    {
        var orders = await _mediator.Send(new GetOrdersQuery(OrderStatusNames.Active, pageSize, pageNumber));
        return View(orders);
    }

    public async Task<IActionResult> AcceptedOrders(int? pageSize, int? pageNumber)
    {
        var orders = await _mediator.Send(new GetOrdersQuery(OrderStatusNames.Accepted, pageSize, pageNumber));
        return View(orders);
    }

    public async Task<IActionResult> CompletedOrders(int? pageSize, int? pageNumber)
    {
        var orders = await _mediator.Send(new GetOrdersQuery(OrderStatusNames.Completed, pageSize, pageNumber));
        return View(orders);
    }
    public async Task<IActionResult> CancelledOrders(int? pageSize, int? pageNumber)
    {
        var orders = await _mediator.Send(new GetOrdersQuery(OrderStatusNames.Cancelled, pageSize, pageNumber));
        return View(orders);
    }

}