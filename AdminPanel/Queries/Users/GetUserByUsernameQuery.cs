using AdminPanel.Models.Identity;
using MediatR;

namespace AdminPanel.Queries.Users
{
    public class GetUserByUsernameQuery : IRequest<ApplicationUser>
    {
        public string Username { get; }
        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }
    }
}
