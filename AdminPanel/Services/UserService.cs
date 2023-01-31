using AdminPanel.Data;
using AdminPanel.Models;

namespace AdminPanel.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.Users;
        }

        public IEnumerable<ApplicationUser> GetUsers(int page, int amount = 10)
        {
            return _context.Users.Skip(page * amount).Take(amount);
        }
    }
}
