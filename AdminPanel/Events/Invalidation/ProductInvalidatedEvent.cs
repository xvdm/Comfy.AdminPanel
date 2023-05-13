using MediatR;

namespace AdminPanel.Events.Invalidation;

public record ProductInvalidatedEvent(int Id, string Url) : INotification;