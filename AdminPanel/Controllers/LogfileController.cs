using AdminPanel.Data;
using AdminPanel.Helpers;
using AdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Manager)]
    public class LogfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LogfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = PoliciesNames.SeniorManager)]
        public IActionResult ShowUserLogs()
        {
            var logs = _context.UserLogs.Include(x => x.LoggingAction).ToList();
            return View("UserLogs", logs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
