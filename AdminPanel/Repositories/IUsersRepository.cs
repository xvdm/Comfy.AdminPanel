using AdminPanel.Models.Identity;

namespace AdminPanel.Repositories
{
    public interface IUsersRepository
    {
        public IQueryable<ApplicationUser> GetUsers(string? searchString);
    }
}
