using AdminPanel.Helpers;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookie").AddCookie("Cookie", config =>
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
