using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace AdminPanel.MediatorHandlers.CacheInvalidation;

public class ProductCacheInvalidationHandler : INotificationHandler<ProductInvalidatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public ProductCacheInvalidationHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task Handle(ProductInvalidatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync($"product:{notification.Id}", cancellationToken);
    }
}