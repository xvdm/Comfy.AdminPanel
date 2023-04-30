using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Logging;
using MediatR;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
[Authorize(Policy = PoliciesNames.SeniorAdministrator)]
public class LoggingController : Controller
{
    private readonly IMediator _mediator;

    public LoggingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> UserLogs(string? searchString, int? pageSize, int? pageNumber)
    {
        var userLogs = await _mediator.Send(new GetUserLogsQuery(searchString, pageSize, pageNumber));
        return View(userLogs);
    }
}