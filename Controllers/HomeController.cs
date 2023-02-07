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

    [Authorize]
    public async Task<IActionResult> Index()
    {
        // List<NewsModel> newsHeadlines  = new List<NewsModel>();
        // newsHeadlines = await _newsAPIService.GetHeadlines();
        // foreach (NewsModel news in newsHeadlines)
        // {
        //     Console.WriteLine($"News output: {news.Title},{news.Description}, {news.PublishedAt} ");
        // }

        return View(await _db.Posts.OrderByDescending(p => p.PostedDate).Include("Author").Include("Likes").Include("Comments").ToListAsync());
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
            FullName = role, //using fullname to store role since its a string

        };
        ViewBag.Message = user;

        return View();
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminPanel()
    {
        List<ApplicationUser> allUsers = await _db.AppUsers.ToListAsync();
        // var posts = await _db.Posts.ToListAsync();

        return View(allUsers);
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

    [HttpGet, ActionName("Like")]
    public async Task<IActionResult> Like(int id)
    {
        if (_db.Posts == null)
        {
            return Problem("Entity set 'AppDbContext.Posts'  is null.");
        }

        var userName = User.Identity.Name;
        var user = _db.Users.Where(u => u.UserName == userName).FirstOrDefault();

        var post = await _db.Posts.FindAsync(id);

        Like newLike = new Like { LikedPost = post, LikedBy = user, LikedDate = DateTime.Now };

        _db.Likes.Add(newLike);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet, ActionName("Unlike")]
    public async Task<IActionResult> Unlike(int id)
    {
        if (_db.Posts == null)
        {
            return Problem("Entity set 'AppDbContext.Posts'  is null.");
        }

        var userName = User.Identity.Name;
        var user = _db.Users.Where(u => u.UserName == userName).FirstOrDefault();

        var post = await _db.Posts.FindAsync(id);

        var like = _db.Likes.Where(l => l.LikedBy == user && l.LikedPost == post).FirstOrDefault();

        if (like != null)
        {
            _db.Likes.Remove(like);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            _logger.LogInformation(string.Empty, $"Like with User Id: {user} and Post Id: {post} not found.");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
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
}
// Helper Methods, may reuse
//Makes the current user into admin



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