using Microsoft.AspNetCore.Authorization;
using AdminPanel.Models.ViewModels;
using AdminPanel.Queries.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AdminPanel.Helpers;
using MediatR;

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
        public async Task<IActionResult> UserLogs()
        {
            var getUserLogsQuery = new GetUserLogsQuery();
            var getUserLogsQueryResult = await _mediator.Send(getUserLogsQuery);
            return View(getUserLogsQueryResult);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}