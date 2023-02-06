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
