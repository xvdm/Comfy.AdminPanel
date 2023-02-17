using Microsoft.AspNetCore.Authorization;
using AdminPanel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AdminPanel.Helpers;
using MediatR;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Administrator)]
    public class AdminPanelController : Controller
    {
        private readonly IMediator _mediator;
        public AdminPanelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
