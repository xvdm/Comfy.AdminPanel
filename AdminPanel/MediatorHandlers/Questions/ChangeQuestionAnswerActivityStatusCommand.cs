using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Questions
{
    public class ChangeQuestionAnswerActivityStatusCommand : IRequest
    {
        public int QuestionAnswerId { get; set; }
        public bool IsActive { get; set; }
        public ChangeQuestionAnswerActivityStatusCommand(int questionAnswerId, bool isActive)
        {
            QuestionAnswerId = questionAnswerId;
            IsActive = isActive;
        }
    }


    public class ChangeQuestionAnswerActivityStatusCommandHandler : IRequestHandler<ChangeQuestionAnswerActivityStatusCommand>
    {
        private readonly ApplicationDbContext _context;

        public ChangeQuestionAnswerActivityStatusCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(ChangeQuestionAnswerActivityStatusCommand request, CancellationToken cancellationToken)
        {
            var questionAnswer = await _context.QuestionAnswers.FirstOrDefaultAsync(x => x.Id == request.QuestionAnswerId, cancellationToken);
            if (questionAnswer is null) throw new HttpRequestException("QuestionAnswer was not found");
            questionAnswer.IsActive = request.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
