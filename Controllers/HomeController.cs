using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Authorization;
using LinkedInClone.Data;

namespace LinkedInClone.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly AppDbContext _db;

    public HomeController(ILogger<HomeController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }
    [Authorize] //testing authorization
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult TestView()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
