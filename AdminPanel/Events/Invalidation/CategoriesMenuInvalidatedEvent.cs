using MediatR;

namespace AdminPanel.Events.Invalidation;

public record CategoriesMenuInvalidatedEvent : INotification;