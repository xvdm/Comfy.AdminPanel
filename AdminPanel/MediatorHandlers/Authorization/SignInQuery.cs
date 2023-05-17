using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.MediatorHandlers.Authorization;

public sealed record SignInQuery(ApplicationUser User, string Password, bool IsPersistent, bool LockoutOnFailure) : IRequest<SignInResult>;


public sealed class SignInQueryHandler : IRequestHandler<SignInQuery, SignInResult>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignInQueryHandler(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<SignInResult> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        return await _signInManager.PasswordSignInAsync(request.User, request.Password, request.IsPersistent, request.LockoutOnFailure);
    }
}