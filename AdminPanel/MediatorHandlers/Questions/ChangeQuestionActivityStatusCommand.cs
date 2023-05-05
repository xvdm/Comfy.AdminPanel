using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Questions;

public record ChangeQuestionActivityStatusCommand(int QuestionId, bool IsActive) : IRequest;


public class ChangeQuestionActivityStatusCommandHandler : IRequestHandler<ChangeQuestionActivityStatusCommand>
{
    private readonly ApplicationDbContext _context;

    public ChangeQuestionActivityStatusCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ChangeQuestionActivityStatusCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == request.QuestionId, cancellationToken);
        if (question is null) throw new HttpRequestException("Question was not found");

        question.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);
    }
}