using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Handlers.Authorization
{
    public class SignInQuery : IRequest<SignInResult>
    {
        public ApplicationUser User { get; }
        public string Password { get; }
        public bool IsPersistent { get; }
        public bool LockoutOnFailure { get; }
        public SignInQuery(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            User = user;
            Password = password;
            IsPersistent = isPersistent;
            LockoutOnFailure = lockoutOnFailure;
        }
    }


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
