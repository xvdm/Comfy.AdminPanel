using AdminPanel.Models.Logging;
using MediatR;

namespace AdminPanel.Queries.Logging
{
    public class GetUserLogsQuery : IRequest<IEnumerable<UserLog>>
    {

    }
}
