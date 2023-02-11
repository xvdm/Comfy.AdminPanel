using AdminPanel.Models;

namespace AdminPanel.Repositories
{
    public interface IUsersRepository
    {
        public IQueryable<ApplicationUser> GetUsers();
        public IQueryable<ApplicationUser> GetUsers(int page, int amountOnPage);
    }
}
