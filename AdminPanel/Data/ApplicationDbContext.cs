using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Models.Logging;
using AdminPanel.Models;

namespace AdminPanel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserLog> UserLogs { get; set; } = null!;
        public DbSet<LoggingAction> LoggingActions { get; set; } = null!;
    }
}
