using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews;

public record GetReviewAnswersQuery : IRequest<IEnumerable<ReviewAnswer>>
{
    public bool IsActive { get; init; }

    private const int MaxPageSize = 15;
    private int _pageSize = MaxPageSize;
    private int _pageNumber = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value is > MaxPageSize or < 1 ? MaxPageSize : value;
    }
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = (value < 1) ? 1 : value;
    }

    public GetReviewAnswersQuery(bool isActive, int? pageSize, int? pageNumber)
    {
        IsActive = isActive;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}


public class GetReviewAnswersHandler : IRequestHandler<GetReviewAnswersQuery, IEnumerable<ReviewAnswer>>
{
    private readonly ApplicationDbContext _context;

    public GetReviewAnswersHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReviewAnswer>> Handle(GetReviewAnswersQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.ReviewAnswers
            .Include(x => x.Review)
                .ThenInclude(x => x.Product)
            .Include(x => x.User)
            .Where(x => x.IsActive == request.IsActive)
            .OrderByDescending(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return result;
    }
}