using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.MediatorHandlers.Users;

public record GetUserByUsernameQuery(string Username) : IRequest<ApplicationUser?>;


public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, ApplicationUser?>
{
    private readonly UserManager<ApplicationUser?> _userManager;

    public GetUserByUsernameQueryHandler(UserManager<ApplicationUser?> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        return await _userManager.FindByNameAsync(request.Username);
    }
}