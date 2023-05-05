using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Questions;

public record GetQuestionsQuery : IRequest<IEnumerable<Question>>
{
    public bool IsActive { get; set; }

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

    public GetQuestionsQuery(bool isActive, int? pageSize, int? pageNumber)
    {
        IsActive = isActive;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}


public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, IEnumerable<Question>>
{
    private readonly ApplicationDbContext _context;

    public GetQuestionsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Question>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Questions
            .Where(x => x.IsActive == request.IsActive)
            .Include(x => x.Answers.Where(y => y.IsActive == true))
                .ThenInclude(x => x.User)
            .Include(x => x.Product)
            .Include(x => x.User)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}