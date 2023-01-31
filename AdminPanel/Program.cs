using AdminPanel.Helpers;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Data;
using AdminPanel.Models;
using AdminPanel.Services;

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
    config.Cookie.Name = "ident";
    config.LoginPath = "/Authorization/Login";
    config.AccessDeniedPath = "/Authorization/AccessDenied";
    config.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    config.SlidingExpiration = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(RolesNames.Administrator, builder =>
    {
        builder.RequireAssertion(x => 
            x.User.HasClaim(ClaimTypes.Role, RolesNames.Administrator) ||
            x.User.HasClaim(ClaimTypes.Role, RolesNames.Manager));
    });
    options.AddPolicy(RolesNames.Manager, builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, RolesNames.Manager));
    });
});

builder.Services.AddControllersWithViews();

builder.Services.AddAntiforgery(config =>
{
    config.FormFieldName = "xcsrf-token";
    config.Cookie.Name = "xcsrf";
});

builder.Services.AddScoped<IUserService, UserService>();

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
