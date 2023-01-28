using AdminPanel.Helpers;
using AdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Policy = RolesNames.Administrator)]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Policy = RolesNames.Manager)]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [Authorize(Policy = RolesNames.Manager)]
        public IActionResult Logs()
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