using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult JavaScriptDisabled()
        {
            return View();
        }
    }
}
