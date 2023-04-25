using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AdminPanel.Models.Identity;
using AdminPanel.Models.Logging;
using System.Security.Claims;
using AdminPanel.Data;

namespace AdminPanel.Services
{
    public class DatabaseLoggerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DatabaseLoggerService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task LogUserAction(ClaimsPrincipal user, Guid subjectUserId, string action)
        {
            var loggingAction = await _context.LoggingActions.FirstOrDefaultAsync(x => x.Action == action);
            if (loggingAction is null) return;

            var userLog = new UserLog() { 
                UserId = Guid.Parse(_userManager.GetUserId(user)), 
                SubjectUserId = subjectUserId, 
                LoggingActionId = loggingAction.Id 
            };

            await _context.UserLogs.AddAsync(userLog);
            await _context.SaveChangesAsync();
        }
    }
}
