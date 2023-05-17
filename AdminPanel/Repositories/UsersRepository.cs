using AdminPanel.Models.Identity;
using AdminPanel.Data;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Repositories;

public sealed class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _context;
    public UsersRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<ApplicationUser> GetUsers(string? searchString)
    {
        var users = _context.Users
            .AsNoTracking()
            .AsQueryable();

        if(searchString is not null)
        {
            users = users.Where(x => 
                x.UserName.Contains(searchString) ||
                x.Email.Contains(searchString) ||
                x.PhoneNumber.Contains(searchString)
            );
        }

        return users;
    }
}