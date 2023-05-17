using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews;

public sealed record GetReviewAnswersTotalCountQuery(bool IsActive) : IRequest<int>;


public sealed class GetReviewAnswersTotalCountQueryHandler : IRequestHandler<GetReviewAnswersTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetReviewAnswersTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetReviewAnswersTotalCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .CountAsync(x => x.IsActive == request.IsActive, cancellationToken);
    }
}