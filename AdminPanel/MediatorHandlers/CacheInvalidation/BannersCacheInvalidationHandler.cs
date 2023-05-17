using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace AdminPanel.MediatorHandlers.CacheInvalidation;

public sealed class BannersCacheInvalidationHandler : INotificationHandler<BannersInvalidatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public BannersCacheInvalidationHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task Handle(BannersInvalidatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync("banners", cancellationToken);
    }
}