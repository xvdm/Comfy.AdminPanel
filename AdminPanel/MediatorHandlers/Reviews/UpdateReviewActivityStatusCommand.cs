using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews;

public record UpdateReviewActivityStatusCommand(int ReviewId, bool IsActive) : IRequest;


public class UpdateReviewActivityStatusCommandHandler : IRequestHandler<UpdateReviewActivityStatusCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public UpdateReviewActivityStatusCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(UpdateReviewActivityStatusCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
        if (review is null) throw new HttpRequestException("Review was not found");

        review.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new ReviewInvalidatedEvent(review.ProductId);
        await _publisher.Publish(notification, cancellationToken);
    }
}