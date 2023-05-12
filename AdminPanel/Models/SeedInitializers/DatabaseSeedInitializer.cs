using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AdminPanel.Helpers;
using AdminPanel.Models.Identity;

namespace AdminPanel.Models.SeedInitializers
{
    public class DatabaseSeedInitializer
    {
        public async Task Seed(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();

            if (userManager is not null)
            {
                var user = new ApplicationUser { UserName = "owner" };
                var userResult = await userManager.CreateAsync(user, "owner");
                if (userResult is not null && userResult.Succeeded) await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, PoliciesNames.Owner));

                user = new ApplicationUser { UserName = "senior" };
                userResult = await userManager.CreateAsync(user, "senior");
                if (userResult is not null && userResult.Succeeded) await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, PoliciesNames.SeniorAdministrator));

                user = new ApplicationUser { UserName = "admin" };
                userResult = await userManager.CreateAsync(user, "admin");
                if (userResult is not null && userResult.Succeeded) await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, PoliciesNames.Administrator));
            }
        }
    }
}
