using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace AdminPanel.MediatorHandlers.CacheInvalidation;

public class CategoriesMenuCacheInvalidationHandler : INotificationHandler<CategoriesMenuInvalidatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public CategoriesMenuCacheInvalidationHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task Handle(CategoriesMenuInvalidatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync("categories-menu", cancellationToken);
    }
}