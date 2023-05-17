using MediatR;

namespace AdminPanel.Events.Invalidation;

public sealed record QuestionInvalidatedEvent(int ProductId) : INotification;