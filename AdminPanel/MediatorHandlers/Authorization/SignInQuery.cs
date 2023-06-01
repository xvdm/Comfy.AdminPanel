using AdminPanel.Helpers;
using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.MediatorHandlers.Authorization;

public sealed record SignInQuery(ApplicationUser User, string Password, bool IsPersistent, bool LockoutOnFailure) : IRequest<bool>;


public sealed class SignInQueryHandler : IRequestHandler<SignInQuery, bool>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public SignInQueryHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.User, request.Password, request.IsPersistent, request.LockoutOnFailure);

        if (result.Succeeded == false) return false;

        var roles = await _userManager.GetRolesAsync(request.User);

        if (roles.Contains(RoleNames.Administrator) || 
            roles.Contains(RoleNames.SeniorAdministrator) ||
            roles.Contains(RoleNames.Owner)) return true;

        return false;
    }
}