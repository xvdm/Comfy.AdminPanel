using AdminPanel.Data;
using AdminPanel.Helpers;
using AdminPanel.Mapping;
using AdminPanel.Models.Identity;
using AdminPanel.Services.Caching;
using AdminPanel.Services.DatabaseLogging;
using AdminPanel.Services.Images.Remove;
using AdminPanel.Services.Images.Upload;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using AdminPanel.Models.SeedInitializers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AdminPanelContextConnection") ?? 
    throw new InvalidOperationException("Connection string 'AdminPanelContextConnection' not found.");



builder.Services.AddDbContext<ApplicationDbContext>(config => 
    config.UseNpgsql(connectionString, opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(UseTestingIdentityConfig)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "ident";
    config.LoginPath = "/Authorization/Login";
    config.AccessDeniedPath = "/Authorization/AccessDenied";
    config.ExpireTimeSpan = TimeSpan.FromDays(1);
    config.SlidingExpiration = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(RoleNames.Owner, policyBuilder =>
    {
        policyBuilder.RequireAssertion(x => 
            x.User.IsInRole(RoleNames.Owner));
    });
    options.AddPolicy(RoleNames.SeniorAdministrator, policyBuilder =>
    {
        policyBuilder.RequireAssertion(x =>
            x.User.IsInRole(RoleNames.Owner) ||
            x.User.IsInRole(RoleNames.SeniorAdministrator));
    });
    options.AddPolicy(RoleNames.Administrator, policyBuilder =>
    {
        policyBuilder.RequireAssertion(x =>
            x.User.IsInRole(RoleNames.Owner) ||
            x.User.IsInRole(RoleNames.SeniorAdministrator) ||
            x.User.IsInRole(RoleNames.Administrator));
    });
});

builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); ;

builder.Services.AddAntiforgery(config =>
{
    config.FormFieldName = "csrf-token";
    config.Cookie.Name = "csrf";
});

builder.Services.AddSingleton(GetConfiguredMappingConfig());
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddScoped<IRemoveImageFromFileSystemService, RemoveImageFromWwwRootService>();
builder.Services.AddScoped<IUploadImageToFileSystemService, UploadImageToWwwRootService>();

builder.Services.AddTransient<DatabaseLoggerService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddScoped<IRemoveCacheService, RemoveRedisCacheService>();

var app = builder.Build();


//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var databaseInit = new DatabaseSeedInitializer();
//    await databaseInit.Seed(services);
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
    pattern: "{controller=Orders}/{action=ActiveOrders}/{id?}");

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