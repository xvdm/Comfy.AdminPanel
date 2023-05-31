using System.Security.Claims;
using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.MediatorHandlers.Users;

public sealed record UpdateUserLockoutStatusCommand(ClaimsPrincipal CurrentUser, Guid UserId, bool IsLockout) : IRequest<bool>;


public sealed class UpdateUserLockoutStatusCommandHandler : IRequestHandler<UpdateUserLockoutStatusCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateUserLockoutStatusCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(UpdateUserLockoutStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) return false;

        if (request.CurrentUser.Identity?.Name == user.UserName) return false;

        if (request.IsLockout)
        {
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(999));
        }
        else
        {
            await _userManager.SetLockoutEndDateAsync(user, null);
        }
        return true;
    }
}