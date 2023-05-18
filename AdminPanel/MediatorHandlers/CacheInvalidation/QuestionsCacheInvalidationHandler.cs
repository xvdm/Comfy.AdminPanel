using AdminPanel.Events.Invalidation;
using AdminPanel.Services.Caching;
using MediatR;

namespace AdminPanel.MediatorHandlers.CacheInvalidation;

public sealed class QuestionsCacheInvalidationHandler : INotificationHandler<QuestionInvalidatedEvent>
{
    private readonly IRemoveCacheService _removeCacheService;

    public QuestionsCacheInvalidationHandler(IRemoveCacheService removeCacheService)
    {
        _removeCacheService = removeCacheService;
    }

    public async Task Handle(QuestionInvalidatedEvent notification, CancellationToken cancellationToken)
    {
        await _removeCacheService.RemoveAsync($"product-questions:{notification.ProductId}");
    }
}