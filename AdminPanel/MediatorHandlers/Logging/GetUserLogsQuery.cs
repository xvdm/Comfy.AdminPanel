using AdminPanel.Models.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Data;

namespace AdminPanel.Handlers.Logging
{
    public class GetUserLogsQuery : IRequest<IEnumerable<UserLog>> 
    {
        private const int _maxPageSize = 15;
        private int _pageSize = _maxPageSize;
        private int _pageNumber = 1;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > _maxPageSize || value < 0) ? _maxPageSize : value;
            }
        }
        public int PageNumber
        {
            get { return _pageNumber; }
            set
            {
                _pageNumber = (value < 1) ? 1 : value;
            }
        }

        public GetUserLogsQuery(int? pageSize, int? pageNumber)
        {
            if(pageSize is not null) PageSize = (int)pageSize;
            if(pageNumber is not null) PageNumber = (int)pageNumber;
        }
    }


    public class GetUserLogsQueryHandler : IRequestHandler<GetUserLogsQuery, IEnumerable<UserLog>>
    {
        private readonly ApplicationDbContext _context;

        public GetUserLogsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserLog>> Handle(GetUserLogsQuery request, CancellationToken cancellationToken)
        {
            var logs = await _context.UserLogs
                .Include(x => x.LoggingAction)
                .OrderByDescending(x => x.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            return logs;
        }
    }
}
