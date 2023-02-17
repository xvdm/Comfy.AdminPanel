using AdminPanel.Models.Identity;

namespace AdminPanel.Repositories
{
    public interface IUsersRepository
    {
        public IQueryable<ApplicationUser> GetUsers();
        public IQueryable<ApplicationUser> GetUsers(int page, int amountOnPage);
    }
}
