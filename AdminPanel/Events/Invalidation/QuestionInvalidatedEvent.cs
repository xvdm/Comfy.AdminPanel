using MediatR;

namespace AdminPanel.Events.Invalidation;

public record QuestionInvalidatedEvent(int ProductId) : INotification;