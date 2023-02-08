using AdminPanel.Data;
using AdminPanel.Models;
using AdminPanel.Models.DTO;
using AdminPanel.Models.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            var loggingAction = await _context.LoggingActions.FirstAsync(x => x.Action == action);
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
