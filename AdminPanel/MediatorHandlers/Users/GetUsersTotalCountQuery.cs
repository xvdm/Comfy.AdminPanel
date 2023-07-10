using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Users;

public sealed record GetUsersTotalCountQuery(string? SearchString, bool LockoutUsers) : IRequest<int>;


public sealed class GetUsersTotalCountQueryHandler : IRequestHandler<GetUsersTotalCountQuery, int>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUsersTotalCountQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<int> Handle(GetUsersTotalCountQuery request, CancellationToken cancellationToken)
    {
        var users = _userManager.Users
            .AsQueryable()
            .Where(x => request.LockoutUsers ? x.LockoutEnd != null : x.LockoutEnd == null);

        if (string.IsNullOrWhiteSpace(request.SearchString) == false)
        {
            users = users.Where(x =>
                x.Name.Contains(request.SearchString) ||
                x.Email.Contains(request.SearchString) ||
                x.PhoneNumber.Contains(request.SearchString)
            );
        }

        return await users.CountAsync(cancellationToken);
    }
}