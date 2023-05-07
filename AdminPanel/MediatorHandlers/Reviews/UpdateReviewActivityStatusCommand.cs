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

        if (review.WasActive == false)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == review.ProductId, cancellationToken);
            if (product is not null)
            {
                var reviewsNumber = await _context.Reviews.CountAsync(x => x.ProductId == review.ProductId && x.WasActive == true, cancellationToken);
                product.Rating = (reviewsNumber * product.Rating + review.ProductRating) / (reviewsNumber + 1);

                var productNotification = new ProductInvalidatedEvent(review.ProductId);
                await _publisher.Publish(productNotification, cancellationToken);
            }
        }

        review.WasActive = true;

        review.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new ReviewInvalidatedEvent(review.ProductId);
        await _publisher.Publish(notification, cancellationToken);
    }
}