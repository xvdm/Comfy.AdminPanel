using MediatR;

namespace AdminPanel.Events.Invalidation;

public sealed record CategoriesMenuInvalidatedEvent : INotification;