using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews;

public record GetReviewsTotalCountQuery(bool IsActive) : IRequest<int>;


public class GetReviewsTotalCountQueryHandler : IRequestHandler<GetReviewsTotalCountQuery, int>
{
    private readonly ApplicationDbContext _context;

    public GetReviewsTotalCountQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetReviewsTotalCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .CountAsync(x => x.IsActive == request.IsActive, cancellationToken);
    }
}