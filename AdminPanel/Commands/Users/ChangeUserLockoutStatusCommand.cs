using System.Security.Claims;
using MediatR;

namespace AdminPanel.Commands.Users
{
    public class ChangeUserLockoutStatusCommand : IRequest<bool>
    {
        public ClaimsPrincipal CurrentUser { get; set; } = null!;
        public Guid UserId { get; set; }
        public bool IsLockout { get; set; }
    }
}
