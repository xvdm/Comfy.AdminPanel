using AdminPanel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AdminPanel.MediatorHandlers.Authorization;
using AdminPanel.MediatorHandlers.Users;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
public sealed class AuthorizationController : Controller
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (string.IsNullOrEmpty(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl) == false)
        {
            model.ReturnUrl = "/";
        }

        var user = await _mediator.Send(new GetUserByUsernameQuery(model.UserName));
        if(user is null)
        {
            ModelState.AddModelError("", "User not found");
            return View(model);
        }

        var result = await _mediator.Send(new SignInQuery(user, model.Password, false, false));
        if(result.Succeeded)
        {
            return LocalRedirect(model.ReturnUrl);
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _mediator.Send(new SignOutQuery());
        return Redirect("Login");
    }

    public IActionResult AccessDenied()
    {
        ViewBag.PageName = HttpContext.Request.Query["ReturnUrl"];
        return View();
    }
}