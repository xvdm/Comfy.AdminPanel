using AdminPanel.Helpers;
using AdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class AdminPanelController : Controller
    {
        public AdminPanelController()
        {

        }

        [Authorize(Policy = RolesNames.Administrator)]
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
