using Microsoft.AspNetCore.Identity;
using AdminPanel.Models.Logging;
using System.Security.Claims;
using AdminPanel.Helpers;
using AdminPanel.Data;

namespace AdminPanel.Models
{
    public class DatabaseSeedInitializer
    {
        public static void Seed(IServiceProvider scopeServiceProvider)
        {
            //var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();

            //var manager = new ApplicationUser
            //{
            //    UserName = "manager"
            //};
            //var managerResult = userManager?.CreateAsync(manager, "manager").GetAwaiter().GetResult();
            //if (managerResult!.Succeeded)
            //{
            //    userManager?.AddClaimAsync(manager, new Claim(ClaimTypes.Role, RolesNames.Manager)).GetAwaiter().GetResult();
            //}

            //var admin = new ApplicationUser
            //{
            //    UserName = "admin"
            //};
            //var adminResult = userManager?.CreateAsync(admin, "admin").GetAwaiter().GetResult();
            //if (adminResult!.Succeeded)
            //{
            //    userManager?.AddClaimAsync(admin, new Claim(ClaimTypes.Role, RolesNames.Administrator)).GetAwaiter().GetResult();
            //}
        }
    }
}
