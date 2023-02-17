using AdminPanel.Models.Identity;
using AdminPanel.Queries.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;

namespace AdminPanel.Handlers.Authorization
{
    public class SignInQueryHandler : IRequestHandler<SignInQuery, SignInResult>
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
}
