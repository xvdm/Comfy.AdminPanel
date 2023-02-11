using AdminPanel.Commands.Logs;
using AdminPanel.Services;
using MediatR;

namespace AdminPanel.Handlers.Logging
{
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
