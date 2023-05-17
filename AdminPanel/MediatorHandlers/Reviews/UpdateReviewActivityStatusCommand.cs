using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews;

public sealed record UpdateReviewActivityStatusCommand(int ReviewId, bool IsActive) : IRequest;


public sealed class UpdateReviewActivityStatusCommandHandler : IRequestHandler<UpdateReviewActivityStatusCommand>
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

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == review.ProductId, cancellationToken);
        if (product is null) return;

        if (request.IsActive)
        {
            product.Rating = (product.ReviewsNumber * product.Rating + review.ProductRating) / (product.ReviewsNumber + 1);
            product.ReviewsNumber += 1;
        }
        else
        {
            if (product.ReviewsNumber == 1) product.Rating = 0;
            else product.Rating = (product.ReviewsNumber * product.Rating - review.ProductRating) / (product.ReviewsNumber - 1);
            product.ReviewsNumber -= 1;
        }

        review.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);

        var productNotification = new ProductInvalidatedEvent(review.ProductId, product.Url);
        await _publisher.Publish(productNotification, cancellationToken);

        var notification = new ReviewInvalidatedEvent(review.ProductId);
        await _publisher.Publish(notification, cancellationToken);
    }
}