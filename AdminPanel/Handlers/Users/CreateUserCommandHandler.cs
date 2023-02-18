using Microsoft.AspNetCore.Identity;
using AdminPanel.Commands.Users;
using System.Security.Claims;
using MediatR;
using Mapster;
using AdminPanel.Models.Identity;

namespace AdminPanel.Handlers.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, request.Role));
                return user.Id;
            }
            return Guid.Empty;
        }
    }
}
