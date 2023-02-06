using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Authorization;
using LinkedInClone.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _db;


    public HomeController(ILogger<HomeController> logger, AppDbContext db, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _db = db;
        _roleManager = roleManager;
    }
    [Authorize]
    public async Task<IActionResult> Index()
    {
      
        return View(await _db.Posts.OrderByDescending(p => p.PostedDate).Include("Author").ToListAsync());
    }
    [Authorize] //testing authorization
    public IActionResult Privacy()
    {
        return View();
    }
    // [HttpPost]
    // public async Task<ActionResult> CreateRoles()
    // {
    //     Console.WriteLine("Attempting to create user role...");
    //     var regUser = new IdentityRole();
    //     regUser.Name = "User";
    //    await _roleManager.CreateAsync(regUser);
    //     Console.WriteLine("user role created!");

    //     Console.WriteLine("Attempting to create recruiter role...");
    //     var recruiter = new IdentityRole();
    //     recruiter.Name = "Recruiter";
    //     await _roleManager.CreateAsync(recruiter);
    //     Console.WriteLine("recruiter role created!");

    //     Console.WriteLine("Attempting to create admin role...");
    //     var admin = new IdentityRole();
    //     admin.Name = "Admin";
    //     await _roleManager.CreateAsync(admin);
    //     Console.WriteLine("admin role created!");

    //     Console.WriteLine("Attempting to save changes...");
    //     int x = await _db.SaveChangesAsync();
    //     if (x > 0){
    //     return RedirectToAction("Index");
    //     }else return RedirectToAction("Privacy");
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> Recruiter()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }

}
