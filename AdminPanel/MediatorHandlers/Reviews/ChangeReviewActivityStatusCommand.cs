using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews
{
    public class ChangeReviewActivityStatusCommand : IRequest
    {
        public int ReviewId { get; set; }
        public bool IsActive { get; set; }
        public ChangeReviewActivityStatusCommand(int reviewId, bool isActive)
        {
            ReviewId = reviewId;
            IsActive = isActive;
        }
    }


    public class ChangeReviewActivityStatusCommandHandler : IRequestHandler<ChangeReviewActivityStatusCommand>
    {
        private readonly ApplicationDbContext _context;

        public ChangeReviewActivityStatusCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(ChangeReviewActivityStatusCommand request, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
            if (review is null) throw new HttpRequestException("Review was not found");
            review.IsActive = request.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
