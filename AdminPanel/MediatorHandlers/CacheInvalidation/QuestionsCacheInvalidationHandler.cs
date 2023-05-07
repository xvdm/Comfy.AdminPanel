using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace AdminPanel.MediatorHandlers.CacheInvalidation;

public class QuestionsCacheInvalidationHandler : INotificationHandler<QuestionInvalidatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public QuestionsCacheInvalidationHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task Handle(QuestionInvalidatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync($"product-questions:{notification.ProductId}", cancellationToken);
    }
}