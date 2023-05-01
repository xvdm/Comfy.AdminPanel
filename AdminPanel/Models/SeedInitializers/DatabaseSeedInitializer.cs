using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AdminPanel.Helpers;
using AdminPanel.Models.Identity;

namespace AdminPanel.Models.SeedInitializers
{
    public class DatabaseSeedInitializer
    {
        public static void Seed(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();

            var owner = new ApplicationUser
            {
                UserName = "owner"
            };
            var ownerResult = userManager?.CreateAsync(owner, "owner").GetAwaiter().GetResult();
            if (ownerResult!.Succeeded)
            {
                userManager?.AddClaimAsync(owner, new Claim(ClaimTypes.Role, PoliciesNames.Owner)).GetAwaiter().GetResult();
            }

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
