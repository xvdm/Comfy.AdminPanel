using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Queries.Authorization
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
}
