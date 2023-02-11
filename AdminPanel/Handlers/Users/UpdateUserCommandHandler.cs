using Microsoft.AspNetCore.Identity;
using AdminPanel.Commands.Users;
using AdminPanel.Exceptions;
using AdminPanel.Models;
using AdminPanel.Data;
using MediatR;

namespace AdminPanel.Handlers.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Id);
            }

            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            await _userManager.UpdateAsync(user);

            if (user.UserName != request.UserName)
            {
                await _userManager.UpdateNormalizedUserNameAsync(user);
            }
            if (user.Email != request.Email)
            {
                await _userManager.UpdateNormalizedEmailAsync(user);
            }
            await _userManager.UpdateSecurityStampAsync(user);

            return true;
        }
    }
}
