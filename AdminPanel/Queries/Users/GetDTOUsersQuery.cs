using AdminPanel.Models.DTO;
using MediatR;

namespace AdminPanel.Queries.Users
{
    public class GetDTOUsersQuery : IRequest<IEnumerable<UserDTO>>
    {
        public bool GetLockoutUsers { get; }

        public GetDTOUsersQuery(bool getLockoutUsers)
        {
            GetLockoutUsers = getLockoutUsers;
        }
    }
}
