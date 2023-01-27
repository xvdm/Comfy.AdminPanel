using AdminPanel.Helpers;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Data;
using AdminPanel.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AdminPanelContextConnection") ?? 
    throw new InvalidOperationException("Connection string 'AdminPanelContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(config => config.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 25))));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
{
    config.SignIn.RequireConfirmedAccount = false;
    //config.Password.RequireNonAlphanumeric = false; // testing only
    //config.Password.RequiredLength = 3;             // testing only
    //config.Password.RequireDigit = false;           // testing only
    //config.Password.RequireUppercase = false;       // testing only
    //config.Password.RequireLowercase = false;       // testing only
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Authorization/Login";
    config.AccessDeniedPath = "/Authorization/AccessDenied";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(RolesHelper.Administrator, builder =>
    {
        builder.RequireAssertion(x => 
            x.User.HasClaim(ClaimTypes.Role, RolesHelper.Administrator) ||
            x.User.HasClaim(ClaimTypes.Role, RolesHelper.Manager));
    });
    options.AddPolicy(RolesHelper.Manager, builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, RolesHelper.Manager));
    });
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())   // seed initializer
//{                                                
//    var services = scope.ServiceProvider;        
//    DatabaseSeedInitializer.Init(services);      
//}                                                

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorization}/{action=Login}/{id?}");

app.Run();
