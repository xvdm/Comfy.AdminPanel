using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace AdminPanel.MediatorHandlers.CacheInvalidation;

public class ReviewsCacheInvalidationHandler : INotificationHandler<ReviewInvalidatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public ReviewsCacheInvalidationHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task Handle(ReviewInvalidatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync($"product-reviews:{notification.ProductId}", cancellationToken);
    }
}