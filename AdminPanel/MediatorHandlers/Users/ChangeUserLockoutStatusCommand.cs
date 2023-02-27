using Microsoft.AspNetCore.Identity;
using AdminPanel.Exceptions;
using MediatR;
using AdminPanel.Models.Identity;
using System.Security.Claims;

namespace AdminPanel.Handlers.Users
{
    public class ChangeUserLockoutStatusCommand : IRequest<bool>
    {
        public ClaimsPrincipal CurrentUser { get; set; } = null!;
        public Guid UserId { get; set; }
        public bool IsLockout { get; set; }
    }


    public class ChangeUserLockoutStatusCommandHandler : IRequestHandler<ChangeUserLockoutStatusCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangeUserLockoutStatusCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ChangeUserLockoutStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.UserId);
            }

            if (request.CurrentUser.Identity?.Name != user.UserName)
            {
                if (request.IsLockout)
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Parse("2999-03-01"));
                }
                else
                {
                    await _userManager.SetLockoutEndDateAsync(user, null);
                }
            }
            return true;
        }
    }
}
