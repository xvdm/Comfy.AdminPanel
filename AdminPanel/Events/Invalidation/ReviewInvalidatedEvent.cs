using MediatR;

namespace AdminPanel.Events.Invalidation;

public record ReviewInvalidatedEvent(int ProductId) : INotification;