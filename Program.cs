using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LinkedInClone.Data;
using Azure.Storage.Blobs;
using LinkedInClone.Services;
using LinkedInClone.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// builder.Services.AddDefaultIdentity<IdentityUser>(options =>
//  options.SignIn.RequireConfirmedAccount = false)
//  .AddEntityFrameworkStores<AppDbContext>();



builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Services needed to store blobs
var blobConnection = builder.Configuration.GetConnectionString("BlobConnectionString");

builder.Services.AddSingleton(x => new BlobServiceClient(blobConnection));

builder.Services.AddSingleton<IBlobService, BlobService>();
builder.Services.AddMvc();
//builder.Services.AddDbContext<
var app = builder.Build();

app.MapRazorPages(); 
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
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized ||
            response.StatusCode == (int)System.Net.HttpStatusCode.Forbidden)
        response.Redirect("/Identity/Account/Login");
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// app.UseEndpoints(endpoints =>
//     {
//         endpoints.MapRazorPages();
//     });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
