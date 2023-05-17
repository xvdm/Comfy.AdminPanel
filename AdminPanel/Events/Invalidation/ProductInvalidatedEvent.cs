using MediatR;

namespace AdminPanel.Events.Invalidation;

public sealed record ProductInvalidatedEvent(int Id, string Url) : INotification;