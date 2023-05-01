using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace AdminPanel.MediatorHandlers.CacheInvalidation;

public class ShowcaseGroupsCacheInvalidationHandler : INotificationHandler<ShowcaseGroupsInvalidatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public ShowcaseGroupsCacheInvalidationHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task Handle(ShowcaseGroupsInvalidatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync("showcase-groups", cancellationToken);
    }
}

