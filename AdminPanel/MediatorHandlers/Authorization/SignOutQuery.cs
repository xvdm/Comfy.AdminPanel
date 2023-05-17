using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.MediatorHandlers.Authorization;

public sealed record SignOutQuery : IRequest;


public sealed class SignOutQueryHandler : IRequestHandler<SignOutQuery>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignOutQueryHandler(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task Handle(SignOutQuery request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
    }
}