using System.Security.Claims;
using MediatR;

namespace AdminPanel.Commands.Logs
{
    public class CreateUserLogCommand : IRequest<bool>
    {
        public ClaimsPrincipal User { get; set; } = null!;
        public Guid SubjetUserId { get; set; }
        public string Action { get; set; } = null!;
    }
}
