using AdminPanel.Services;
using MediatR;
using System.Security.Claims;

namespace AdminPanel.Handlers.Logging
{
    public class CreateUserLogCommand : IRequest<bool>
    {
        public ClaimsPrincipal User { get; set; } = null!;
        public Guid SubjetUserId { get; set; }
        public string Action { get; set; } = null!;
        public CreateUserLogCommand(ClaimsPrincipal user, Guid subjetUserId, string action)
        {
            User = user;
            SubjetUserId = subjetUserId;
            Action = action;
        }
    }


    public class CreateUserLogCommandHandler : IRequestHandler<CreateUserLogCommand, bool>
    {
        private readonly DatabaseLoggerService _databaseLoggerService;

        public CreateUserLogCommandHandler(DatabaseLoggerService databaseLoggerService)
        {
            _databaseLoggerService = databaseLoggerService;
        }

        public async Task<bool> Handle(CreateUserLogCommand request, CancellationToken cancellationToken)
        {
            await _databaseLoggerService.LogUserAction(request.User, request.SubjetUserId, request.Action);
            return true;
        }
    }
}
