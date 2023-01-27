using AdminPanel.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AdminPanel.Models
{
    public class DatabaseSeedInitializer
    {
        public static void Init(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();

            var manager = new ApplicationUser
            {
                UserName = "manager"
            };
            var managerResult = userManager?.CreateAsync(manager, "manager").GetAwaiter().GetResult();
            if (managerResult!.Succeeded)
            {
                userManager?.AddClaimAsync(manager, new Claim(ClaimTypes.Role, RolesHelper.Manager)).GetAwaiter().GetResult();
            }

            var admin = new ApplicationUser
            {
                UserName = "admin"
            };
            var adminResult = userManager?.CreateAsync(admin, "admin").GetAwaiter().GetResult();
            if (adminResult!.Succeeded)
            {
                userManager?.AddClaimAsync(admin, new Claim(ClaimTypes.Role, RolesHelper.Administrator)).GetAwaiter().GetResult();
            }
        }
    }
}
