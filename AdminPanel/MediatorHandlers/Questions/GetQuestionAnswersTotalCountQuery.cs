using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Questions;

public record GetQuestionAnswersTotalCountQuery(bool IsActive) : IRequest<int>;


public class GetQuestionAnswersTotalCountQueryHandler : IRequestHandler<GetQuestionAnswersTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetQuestionAnswersTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetQuestionAnswersTotalCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.Questions
            .CountAsync(x => x.IsActive == request.IsActive, cancellationToken);
    }
}