using AdminPanel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AuthorizationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthorizationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (String.IsNullOrEmpty(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl) == false)
            {
                model.ReturnUrl = "/Authorization/Login";
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if(result.Succeeded)
            {
                return LocalRedirect(model.ReturnUrl);
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            ViewBag.PageName = HttpContext.Request.Query["ReturnUrl"];
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("Login");
        }
    }
}
