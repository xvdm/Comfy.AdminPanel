using AdminPanel.Models;

namespace AdminPanel.Services
{
    public interface IUserService
    {
        public IEnumerable<ApplicationUser> GetUsers();
        public IEnumerable<ApplicationUser> GetUsers(int page, int amount = 10);
    }
}
