using MediatR;

namespace AdminPanel.Events.Invalidation;

public record BannersInvalidatedEvent : INotification;