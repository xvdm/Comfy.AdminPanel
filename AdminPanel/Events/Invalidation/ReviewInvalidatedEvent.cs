using MediatR;

namespace AdminPanel.Events.Invalidation;

public sealed record ReviewInvalidatedEvent(int ProductId) : INotification;