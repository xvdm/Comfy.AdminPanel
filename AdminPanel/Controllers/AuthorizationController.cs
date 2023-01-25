using AdminPanel.Helpers;
using AdminPanel.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace AdminPanel.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Login(string? returnUrl)
        {
            if (String.IsNullOrEmpty(returnUrl) || Url.IsLocalUrl(returnUrl) == false)
                returnUrl = "/Authorization/Login";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.Login),
                new Claim(ClaimTypes.Role, RolesHelper.Manager)
            };
            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPrincipal);

            return LocalRedirect(model.ReturnUrl);
        }

        public IActionResult AccessDenied()
        {
            ViewBag.PageName = HttpContext.Request.Query["ReturnUrl"];
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookie");
            return Redirect("Login");
        }
    }
}
