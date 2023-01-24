using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Login()
        {
            Console.WriteLine("Login");
            return View();
        }

        public IActionResult Logout()
        {
            Console.WriteLine("Logout");
            return View("Login");
        }
    }
}
