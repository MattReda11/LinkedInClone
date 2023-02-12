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



    public HomeController(ILogger<HomeController> logger, AppDbContext db, RoleManager<IdentityRole> roleManager,
     UserManager<ApplicationUser> userManager, INewsAPIService newsAPIService)
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
            //News API
            NewsResponse apiResponse = await _newsAPIService.GetHeadlines();
            var articles = apiResponse.articles;
            ViewBag.Articles = articles;
            //Stocks API
            var finnhubClient = new FinnhubClient("cfj7nn1r01que34nrafgcfj7nn1r01que34nrag0");
            var symbols = new[] { "AAPL", "GOOG", "MSFT", "AMZN" };
            var quotes = await finnhubClient.GetQuotesAsync(symbols);
            ViewBag.Stocks = quotes;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        List<Post> Posts = new List<Post>();

        var username = User.Identity.Name;
        var user = _db.Users.Where(u => u.UserName == username).FirstOrDefault();

        var connections1 = _db.Connections.Where(c => c.Accepted == true && c.AccountOwner == user).Include("AccountOwner").Include("Friend").ToList();

        var connections2 = _db.Connections.Where(c => c.Accepted == true && c.Friend == user).Include("AccountOwner").Include("Friend").ToList();

        var userPosts = _db.Posts.Where(p => p.Author == user).Include("Author").Include("Comments").Include("Comments.Author").Include("Likes").Include("Likes.LikedBy").ToList();

        foreach (var userPost in userPosts)
        {
            Posts.Add(userPost);
        }

        foreach (var con in connections1)
        {
            var posts = await _db.Posts.Where(p => p.Author == con.Friend).Include("Author").Include("Comments").Include("Comments.Author").Include("Likes").Include("Likes.LikedBy").ToListAsync();


            foreach (var post in posts)
            {
                Posts.Add(post);
            }
        }

        foreach (var con in connections2)
        {
            var posts = await _db.Posts.Where(p => p.Author == con.AccountOwner).Include("Author").Include("Comments").Include("Comments.Author").Include("Likes").Include("Likes.LikedBy").ToListAsync();


            foreach (var post in posts)
            {
                Posts.Add(post);
            }
        }
        return View(Posts.OrderByDescending(p => p.PostedDate));
    }

    [Authorize]
    public async Task<IActionResult> MyAccount()
    {
        //not the best solution, will try to optimize later
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        var checkRecruiter = await _userManager.IsInRoleAsync(user, "Recruiter");
        var checkAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        var role = "User";
        if (checkRecruiter)
        {
            role = "Recruiter";
        }
        else if (checkAdmin)
        {
            role = "Admin";
        }

        ApplicationUser retrievedData = new ApplicationUser
        {
            Id = userId,
            UserName = user.UserName,
            FullName = user.FullName,
            RoleId = role,

        };
        ViewBag.Message = retrievedData;

        return View("~/Views/Account/MyAccount.cshtml");
    }


    [Authorize(Roles = "Admin")]
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
        try
        {
            NewsResponse apiResponse = await _newsAPIService.GetHeadlines();
            var articles = apiResponse.articles;
            ViewBag.Articles = articles;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
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