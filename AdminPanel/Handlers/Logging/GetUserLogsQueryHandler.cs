using AdminPanel.Queries.Logging;
using AdminPanel.Models.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Data;

namespace AdminPanel.Handlers.Logging
{
    public class GetUserLogsQueryHandler : IRequestHandler<GetUserLogsQuery, IEnumerable<UserLog>>
    {
        private readonly ApplicationDbContext _context;

        public GetUserLogsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserLog>> Handle(GetUserLogsQuery request, CancellationToken cancellationToken)
        {
            var logs = await _context.UserLogs.Include(x => x.LoggingAction).ToListAsync(cancellationToken);
            return logs;
        }
    }
}
