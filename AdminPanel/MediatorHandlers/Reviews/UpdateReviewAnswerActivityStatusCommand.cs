using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews;

public sealed record UpdateReviewAnswerActivityStatusCommand(int ReviewAnswerId, bool IsActive) : IRequest;


public sealed class UpdateReviewAnswerActivityStatusCommandHandler : IRequestHandler<UpdateReviewAnswerActivityStatusCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public UpdateReviewAnswerActivityStatusCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(UpdateReviewAnswerActivityStatusCommand request, CancellationToken cancellationToken)
    {
        var reviewAnswer = await _context.ReviewAnswers
            .Include(x => x.Review)
            .FirstOrDefaultAsync(x => x.Id == request.ReviewAnswerId, cancellationToken);
        if (reviewAnswer is null) throw new HttpRequestException("Review answer was not found");

        reviewAnswer.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);
    }
}