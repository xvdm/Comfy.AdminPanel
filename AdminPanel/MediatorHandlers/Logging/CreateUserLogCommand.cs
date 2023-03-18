using AdminPanel.Services;
using MediatR;
using System.Security.Claims;

namespace AdminPanel.Handlers.Logging
{
    public class CreateUserLogCommand : IRequest<bool>
    {
        public ClaimsPrincipal User { get; set; }
        public Guid SubjectUserId { get; set; }
        public string Action { get; set; }
        public CreateUserLogCommand(ClaimsPrincipal user, Guid subjectUserId, string action)
        {
            User = user;
            SubjectUserId = subjectUserId;
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
            await _databaseLoggerService.LogUserAction(request.User, request.SubjectUserId, request.Action);
            return true;
        }
    }
}
