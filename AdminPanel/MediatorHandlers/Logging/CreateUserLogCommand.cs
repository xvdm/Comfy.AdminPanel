using System.Security.Claims;
using AdminPanel.Services.DatabaseLogging;
using MediatR;

namespace AdminPanel.MediatorHandlers.Logging;

public sealed record CreateUserLogCommand(ClaimsPrincipal User, Guid SubjectUserId, string Action) : IRequest<bool>;


public sealed class CreateUserLogCommandHandler : IRequestHandler<CreateUserLogCommand, bool>
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