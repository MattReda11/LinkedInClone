using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LinkedInClone.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LinkedInCloneIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'LinkedInCloneIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<LinkedInCloneIdentityDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LinkedInCloneIdentityDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<
var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;

//     SeedData.Initialize(services);
// }

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
    {
        endpoints.MapRazorPages();
    });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
