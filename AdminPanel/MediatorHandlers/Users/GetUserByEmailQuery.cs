using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.MediatorHandlers.Users;

public sealed record GetUserByEmailQuery(string Email) : IRequest<ApplicationUser?>;


public sealed class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, ApplicationUser?>
{
    private readonly UserManager<ApplicationUser?> _userManager;

    public GetUserByEmailQueryHandler(UserManager<ApplicationUser?> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _userManager.FindByEmailAsync(request.Email);
    }
}