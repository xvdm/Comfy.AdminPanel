using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.Helpers;
using MediatR;
using AdminPanel.Handlers.Logging;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Manager)]
    public class LoggingController : Controller
    {
        private readonly IMediator _mediator;

        public LoggingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = PoliciesNames.SeniorManager)]
        public async Task<IActionResult> UserLogs(int? pageSize, int? pageNumber)
        {
            var userLogs = await _mediator.Send(new GetUserLogsQuery(pageSize, pageNumber));
            return View(userLogs);
        }
    }
}