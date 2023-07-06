using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Questions;

public sealed record UpdateQuestionAnswerActivityStatusCommand(int QuestionAnswerId, bool IsActive) : IRequest;


public sealed class UpdateQuestionAnswerActivityStatusCommandHandler : IRequestHandler<UpdateQuestionAnswerActivityStatusCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public UpdateQuestionAnswerActivityStatusCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(UpdateQuestionAnswerActivityStatusCommand request, CancellationToken cancellationToken)
    {
        var questionAnswer = await _context.QuestionAnswers
            .Include(x => x.Question)
            .FirstOrDefaultAsync(x => x.Id == request.QuestionAnswerId, cancellationToken);
        if (questionAnswer is null) throw new HttpRequestException("Question answer was not found");

        questionAnswer.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);
    }
}