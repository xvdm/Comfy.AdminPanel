using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews
{
    public class ChangeReviewAnswerActivityStatusCommand : IRequest
    {
        public int ReviewAnswerId { get; set; }
        public bool IsActive { get; set; }
        public ChangeReviewAnswerActivityStatusCommand(int reviewAnswerId, bool isActive)
        {
            ReviewAnswerId = reviewAnswerId;
            IsActive = isActive;
        }
    }


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
            if (reviewAnswer is null) throw new HttpRequestException("ReviewAnswer was not found");
            reviewAnswer.IsActive = request.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
