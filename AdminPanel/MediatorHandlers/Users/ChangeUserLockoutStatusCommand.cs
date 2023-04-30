using System.Security.Claims;
using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.MediatorHandlers.Users;

public record ChangeUserLockoutStatusCommand(ClaimsPrincipal CurrentUser, Guid UserId, bool IsLockout) : IRequest<bool>;


public class ChangeUserLockoutStatusCommandHandler : IRequestHandler<ChangeUserLockoutStatusCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ChangeUserLockoutStatusCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(ChangeUserLockoutStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            throw new HttpRequestException("User with given id was not found");
        }

        if (request.CurrentUser.Identity?.Name != user.UserName)
        {
            if (request.IsLockout)
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Parse("2999-03-01"));
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
            }
        }
        return true;
    }
}