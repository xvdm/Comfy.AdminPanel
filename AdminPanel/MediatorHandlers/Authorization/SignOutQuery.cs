using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.MediatorHandlers.Authorization;

public record SignOutQuery : IRequest;


public class SignOutQueryHandler : IRequestHandler<SignOutQuery>
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