using AdminPanel.Models.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.MediatorHandlers.Users;

public sealed record CreateUserCommand : IRequest<Guid>
{
    public string Role { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}


public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if(request.Password != request.ConfirmPassword) return Guid.Empty;

        var user = request.Adapt<ApplicationUser>();
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded == false) return Guid.Empty;

        await _userManager.AddToRoleAsync(user, request.Role);
        return user.Id;
    }
}