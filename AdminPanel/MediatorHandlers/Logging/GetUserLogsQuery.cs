using AdminPanel.Data;
using AdminPanel.Models.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Logging;

public sealed record GetUserLogsQuery : IRequest<IEnumerable<UserLog>> 
{
    public string? SearchString { get; set; }

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

    public GetUserLogsQuery(string? searchString, int? pageSize, int? pageNumber)
    {
        SearchString = searchString;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}


public sealed class GetUserLogsQueryHandler : IRequestHandler<GetUserLogsQuery, IEnumerable<UserLog>>
{
    private readonly ApplicationDbContext _context;

    public GetUserLogsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserLog>> Handle(GetUserLogsQuery request, CancellationToken cancellationToken)
    {
        var logs = _context.UserLogs
            .AsQueryable();


        if(request.SearchString is not null)
        {
            logs = logs.Where(x => 
                x.UserId.ToString().Contains(request.SearchString) ||
                x.SubjectUserId.ToString().Contains(request.SearchString) ||
                x.LoggingAction.Action.Contains(request.SearchString));
        }


        return await logs
            .Include(x => x.LoggingAction)
            .OrderByDescending(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}