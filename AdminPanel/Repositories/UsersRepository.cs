using AdminPanel.Models;
using AdminPanel.Data;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return _context.Users;
        }

        public IQueryable<ApplicationUser> GetUsers(int page, int amountOnPage = 10)
        {
            return _context.Users.Skip(page * amountOnPage).Take(amountOnPage);
        }
    }
}
