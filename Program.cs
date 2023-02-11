using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LinkedInClone.Data;
using Azure.Storage.Blobs;
using LinkedInClone.Services;
using LinkedInClone.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
using LinkedInClone;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();


builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.ConfigureApplicationCookie(options =>
   {
       // Cookie settings
       // options.Cookie.HttpOnly = true;
       // options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

       // options.LoginPath = "/Account/Login";
       options.AccessDeniedPath = "/Home/AccessDenied";
       // options.SlidingExpiration = true;`
   });

// Add services to the container.
builder.Services.AddControllersWithViews();

// Services needed to store blobs
var blobConnection = builder.Configuration.GetConnectionString("BlobConnectionString");

builder.Services.AddSingleton(x => new BlobServiceClient(blobConnection));
builder.Services.AddSingleton<INewsAPIService, NewsAPIService>();
builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddMvc();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.OnAppendCookie = cookieContext =>
        cookieContext.CookieOptions.Secure = true;
    options.OnDeleteCookie = cookieContext =>
        cookieContext.CookieOptions.Secure = true;
});

builder.Services.AddSignalR();
// builder.Services.AddTransient<IEmailSender, EmailSender>();
//builder.Services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("AuthMessageSenderOptions"));

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "379601028963-alml822od0odsmo04m5hl4png6ikqasp.apps.googleusercontent.com"; //builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = "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";
    googleOptions.SignInScheme = IdentityConstants.ExternalScheme;
    googleOptions.SaveTokens = true;

});

var app = builder.Build();

app.MapRazorPages();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//redirects to login page if any restricted url is accessed
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized ||
            response.StatusCode == (int)System.Net.HttpStatusCode.Forbidden ||
            response.StatusCode == (int)System.Net.HttpStatusCode.NotFound)
        response.Redirect("/Identity/Account/Login");
});
// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "admin",
        pattern: "{controller=Admin}/{action=AdminPanel}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
