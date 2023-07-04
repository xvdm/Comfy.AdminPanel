using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Orders;
using AdminPanel.Models.Entities;
using AdminPanel.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
[Authorize(Policy = RoleNames.Administrator)]
public sealed class OrdersController : Controller
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Orders(OrderStatusEnum? statusFilter, int? pageSize, int? pageNumber)
    {
        var statuses = await _mediator.Send(new GetOrderStatusesQuery());
        var query = new GetOrdersQuery(statusFilter, pageSize, pageNumber);
        var orders = await _mediator.Send(query);

        var totalCount = await _mediator.Send(new GetOrdersTotalCountQuery(statusFilter));
        var totalPages = (totalCount - 1) / query.PageSize + 1;

        var viewModel = new OrdersViewModel
        {
            Orders = orders,
            Statuses = statuses,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber
        };
        return View(viewModel);
    }

    public async Task<IActionResult> ChangeOrderStatus(int id, OrderStatusEnum currentStatus)
    {
        var statuses = await _mediator.Send(new GetOrderStatusesQuery());
        var status = statuses.FirstOrDefault(x => x.Id == (int)currentStatus)?.ToString();
        if (status is null) throw new HttpRequestException();

        var viewModel = new ChangeOrderStatusViewModel
        {
            OrderId = id,
            OrderStatus = status,
            Statuses = statuses
        };
        return View(viewModel);
    }

    public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatusEnum newStatus)
    {
        await _mediator.Send(new UpdateOrderStatusCommand(id, newStatus));
        return RedirectToAction(nameof(Orders));
    }
}