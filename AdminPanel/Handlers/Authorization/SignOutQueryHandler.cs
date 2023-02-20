using AdminPanel.Models.Identity;
using AdminPanel.Queries.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Handlers.Authorization
{
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
            return;
        }
    }
}
