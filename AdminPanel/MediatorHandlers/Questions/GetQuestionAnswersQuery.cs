using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Questions;

public sealed record GetQuestionAnswersQuery : IRequest<IEnumerable<QuestionAnswer>>
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

    public GetQuestionAnswersQuery(bool isActive, int? pageSize, int? pageNumber)
    {
        IsActive = isActive;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}


public sealed class GetQuestionAnswersQueryHandler : IRequestHandler<GetQuestionAnswersQuery, IEnumerable<QuestionAnswer>>
{
    private readonly ApplicationDbContext _context;

    public GetQuestionAnswersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuestionAnswer>> Handle(GetQuestionAnswersQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.QuestionAnswers
            .Include(x => x.Question)
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