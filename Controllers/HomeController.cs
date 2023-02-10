using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Authorization;
using LinkedInClone.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using LinkedInClone.Services;

namespace LinkedInClone.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly INewsAPIService _newsAPIService;


    public HomeController(ILogger<HomeController> logger, AppDbContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, INewsAPIService newsAPIService)
    {
        _logger = logger;
        _db = db;
        _roleManager = roleManager;
        _userManager = userManager;
        _newsAPIService = newsAPIService;
    }

    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> Index()
    {
        try
        {
            NewsResponse apiResponse = await _newsAPIService.GetHeadlines();
            var articles = apiResponse.articles;
            ViewBag.Articles = articles;
            // int count = 0;
            // foreach (NewsModel news in articles)
            // {
            //     //just to see if its working
            //     Console.WriteLine($"News output #{count}: {news.title},{news.description}, {news.publishedAt} ");
            //     count++;
            //     if (count >= 5) break;
            // }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return View(await _db.Posts.OrderByDescending(p => p.PostedDate).Include("Author").Include("Comments").Include("Likes").Include("Likes.LikedBy").ToListAsync());
    }

    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }
    [Authorize]
    public async Task<IActionResult> MyAccount()
    {
        //not the best solution, will try to optimize later
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var res = _db.AppUsers.Where(u => (u.Id == userId)).FirstOrDefault(); //u.Id == userId
        var checkRecruiter = await _userManager.IsInRoleAsync(res, "Recruiter");
        var checkAdmin = await _userManager.IsInRoleAsync(res, "Admin");
        var role = "User";
        if (checkRecruiter)
        {
            role = "Recruiter";
        }
        else if (checkAdmin)
        {
            role = "Admin";
        }

        ApplicationUser user = new ApplicationUser
        {
            Id = userId,
            Email = userName,
            FullName = res.FullName,
            RoleId = role,

        };
        ViewBag.Message = user;

        return View("~/Views/Account/MyAccount.cshtml");
    }



    public async Task<IActionResult> AdminPanel()
    {
        var model = new AdminPanelViewModel();
        model.Users = await _db.AppUsers.ToListAsync();
        model.JobPostings = await _db.JobPostings.ToListAsync();
        model.Posts = await _db.Posts.ToListAsync();
        model.Comments = await _db.Comments.ToListAsync();
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> Recruiter()
    {
        var username = User.Identity.Name;
        var user = _db.Users.Where(u => u.UserName == username).FirstOrDefault();

        return View(await _db.JobPostings.Where(j => j.Recruiter == user).Include("JobApplications").ToListAsync());
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet, ActionName("Message")]
    public IActionResult Message()
    {
        return View();
    }

}
// Helper Methods, may reuse
//Makes the current user into admin
//     [HttpPost]
//     public async Task<RedirectToActionResult> AdminX()
//     {
//         var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//         var res = _db.AppUsers.Where(u => (u.Id == userId)).FirstOrDefault(); //u.Id == userId
//         try
//         {
//             Console.WriteLine("adding user to role...");
//             await _userManager.AddToRoleAsync(res, "Admin");
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine(ex.Message);
//         }
//         try
//         {
//             Console.WriteLine("Removing user from role...");
//             await _userManager.RemoveFromRoleAsync(res, "User");
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine(ex.Message);
//         }
//         return RedirectToAction("Index","Home");
// }
//      catch (Exception ex)
//     {
//         Console.WriteLine(ex.Message);
//     }
//     return RedirectToAction("Index");
// }

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