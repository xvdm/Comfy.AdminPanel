using MediatR;

namespace AdminPanel.Events.Invalidation;

public sealed record BannersInvalidatedEvent : INotification;