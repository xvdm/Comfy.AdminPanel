using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews;

public record ChangeReviewAnswerActivityStatusCommand(int ReviewAnswerId, bool IsActive) : IRequest;


public class ChangeReviewAnswerActivityStatusCommandHandler : IRequestHandler<ChangeReviewAnswerActivityStatusCommand>
{
    private readonly ApplicationDbContext _context;

    public ChangeReviewAnswerActivityStatusCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ChangeReviewAnswerActivityStatusCommand request, CancellationToken cancellationToken)
    {
        var reviewAnswer = await _context.ReviewAnswers.FirstOrDefaultAsync(x => x.Id == request.ReviewAnswerId, cancellationToken);
        if (reviewAnswer is null) throw new HttpRequestException("Review answer was not found");

        reviewAnswer.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);
    }
}