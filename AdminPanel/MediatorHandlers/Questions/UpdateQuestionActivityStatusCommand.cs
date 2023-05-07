using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Questions;

public record UpdateQuestionActivityStatusCommand(int QuestionId, bool IsActive) : IRequest;


public class UpdateQuestionActivityStatusCommandHandler : IRequestHandler<UpdateQuestionActivityStatusCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public UpdateQuestionActivityStatusCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(UpdateQuestionActivityStatusCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == request.QuestionId, cancellationToken);
        if (question is null) throw new HttpRequestException("Question was not found");

        question.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new QuestionInvalidatedEvent(question.ProductId);
        await _publisher.Publish(notification, cancellationToken);
    }
}