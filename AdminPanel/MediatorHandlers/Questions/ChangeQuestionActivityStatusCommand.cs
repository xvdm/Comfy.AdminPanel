using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Questions
{
    public class ChangeQuestionActivityStatusCommand : IRequest
    {
        public int QuestionAnswerId { get; set; }
        public bool IsActive { get; set; }
        public ChangeQuestionActivityStatusCommand(int questionId, bool isActive)
        {
            QuestionAnswerId = questionId;
            IsActive = isActive;
        }
    }


    public class ChangeQuestionActivityStatusCommandHandler : IRequestHandler<ChangeQuestionActivityStatusCommand>
    {
        private readonly ApplicationDbContext _context;

        public ChangeQuestionActivityStatusCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(ChangeQuestionActivityStatusCommand request, CancellationToken cancellationToken)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == request.QuestionAnswerId, cancellationToken);
            if (question is null) throw new HttpRequestException("Question was not found");
            question.IsActive = request.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
