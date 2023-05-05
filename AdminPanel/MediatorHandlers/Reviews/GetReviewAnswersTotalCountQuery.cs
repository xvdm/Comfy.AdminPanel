using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews;

public record GetReviewAnswersTotalCountQuery(bool IsActive) : IRequest<int>;


public class GetReviewAnswersTotalCountQueryHandler : IRequestHandler<GetReviewAnswersTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetReviewAnswersTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetReviewAnswersTotalCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .Where(x => x.IsActive == request.IsActive)
            .CountAsync(cancellationToken);
    }
}