using AdminPanel.Helpers;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Data;
using AdminPanel.Models;
using AdminPanel.Services;
using MapsterMapper;
using Mapster;
using System.Drawing.Text;
using AdminPanel.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AdminPanelContextConnection") ?? 
    throw new InvalidOperationException("Connection string 'AdminPanelContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(config => config.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 25))));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(config => UseTestingIdentityConfig(config))
    .AddEntityFrameworkStores<ApplicationDbContext>();

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
    options.AddPolicy(PoliciesNames.Owner, builder =>
    {
        builder.RequireAssertion(x => 
            x.User.HasClaim(ClaimTypes.Role, PoliciesNames.Owner));
    });
    options.AddPolicy(PoliciesNames.SeniorManager, builder =>
    {
        builder.RequireAssertion(x =>
            x.User.HasClaim(ClaimTypes.Role, PoliciesNames.Owner) ||
            x.User.HasClaim(ClaimTypes.Role, PoliciesNames.SeniorManager));
    });
    options.AddPolicy(PoliciesNames.Manager, builder =>
    {
        builder.RequireAssertion(x =>
            x.User.HasClaim(ClaimTypes.Role, PoliciesNames.Owner) ||
            x.User.HasClaim(ClaimTypes.Role, PoliciesNames.SeniorManager) ||
            x.User.HasClaim(ClaimTypes.Role, PoliciesNames.Manager));
    });
    options.AddPolicy(PoliciesNames.Administrator, builder =>
    {
        builder.RequireAssertion(x =>
            x.User.HasClaim(ClaimTypes.Role, PoliciesNames.Owner) ||
            x.User.HasClaim(ClaimTypes.Role, PoliciesNames.SeniorManager) ||
            x.User.HasClaim(ClaimTypes.Role, PoliciesNames.Manager) ||
            x.User.HasClaim(ClaimTypes.Role, PoliciesNames.Administrator));
    });
});

builder.Services.AddControllersWithViews();

builder.Services.AddAntiforgery(config =>
{
    config.FormFieldName = "xcsrf-token";
    config.Cookie.Name = "xcsrf";
});

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<DTOService>();

builder.Services.AddSingleton(GetConfiguredMappingConfig());
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddTransient<DatabaseLoggerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())   // seed initializer
{
    var services = scope.ServiceProvider;
    DatabaseSeedInitializer.Seed(services);
}

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


static TypeAdapterConfig GetConfiguredMappingConfig()
{
    var config = new TypeAdapterConfig();
    new RegisterMapper().Register(config);
    return config;
}

static void UseTestingIdentityConfig(IdentityOptions config)
{
    config.SignIn.RequireConfirmedAccount = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false; 
    config.Password.RequireLowercase = true;
    config.Password.RequiredLength = 3;
    config.Password.RequireDigit = false;
}

//static void UseProductionIdentityConfig(IdentityOptions config)
//{
//    config.SignIn.RequireConfirmedAccount = false;
//    config.Password.RequireNonAlphanumeric = true;
//    config.Password.RequireUppercase = true;
//    config.Password.RequireLowercase = true;
//    config.Password.RequiredLength = 10;
//    config.Password.RequireDigit = true;
//}