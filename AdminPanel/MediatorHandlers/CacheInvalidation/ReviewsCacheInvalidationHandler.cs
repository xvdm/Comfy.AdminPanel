using AdminPanel.Events.Invalidation;
using AdminPanel.Services.Caching;
using MediatR;

namespace AdminPanel.MediatorHandlers.CacheInvalidation;

public sealed class ReviewsCacheInvalidationHandler : INotificationHandler<ReviewInvalidatedEvent>
{
    private readonly IRemoveCacheService _removeCacheService;

    public ReviewsCacheInvalidationHandler(IRemoveCacheService removeCacheService)
    {
        _removeCacheService = removeCacheService;
    }

    public async Task Handle(ReviewInvalidatedEvent notification, CancellationToken cancellationToken)
    {
        await _removeCacheService.RemoveAsync($"product-reviews:{notification.ProductId}");
    }
}