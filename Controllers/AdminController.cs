using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Authorization;
using LinkedInClone.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LinkedInClone.Controllers;
[Area("Admin")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _db;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;


    public AdminController(ILogger<AdminController> logger, AppDbContext db, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _db = db;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    
    
  
    [HttpDelete("/Admin/DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            Console.WriteLine($"User with ID:{id} does not exist!");
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            Console.WriteLine("User deleted successfully!");
            return RedirectToAction("AdminPanel", "Home");
        }

        return BadRequest();
    }

    [HttpDelete("/Admin/DeletePost/{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        
        var post = await _db.Posts.FindAsync(id);  //await _userManager.FindByIdAsync(id);
        if (post == null)
        {
            Console.WriteLine($"Post with ID:{id} does not exist!");
            return NotFound();
        }
        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();

        Console.WriteLine($"Post with ID:{id} has been deleted");
        return RedirectToAction("AdminPanel", "Home");
    }

    [HttpDelete("/Admin/DeleteJobPosting/{id}")]
    public async Task<IActionResult> DeleteJobPosting(int id)
    {
        var jobPost = await _db.JobPostings.FindAsync(id);
        if (jobPost == null)
        {
            Console.WriteLine($"Job Post with ID:{id} does not exist!");
            return NotFound();
        }
        _db.JobPostings.Remove(jobPost);
        await _db.SaveChangesAsync();

        Console.WriteLine($"Job Post with ID:{id} has been deleted");
        return RedirectToAction("AdminPanel", "Home");      
    }


    [HttpDelete("/Admin/DeleteComment/{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _db.Comments.FindAsync(id);
        if (comment == null)
        {
            Console.WriteLine($"Comment with ID:{id} does not exist!");
            return NotFound();
        }
        _db.Comments.Remove(comment);
        await _db.SaveChangesAsync();

        Console.WriteLine($"Comment with ID:{id} has been deleted");
        return RedirectToAction("AdminPanel", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
//     public IActionResult LoadUsersTable()
//     {
//         var users = _db.AppUsers.ToList();
//         return PartialView("_UsersTable", users);
//     }

//     public IActionResult LoadUserPostsTable()
//     {
//         var posts = _db.Posts.ToList();
//         return PartialView("_UserPostsTable", posts);
//     }

//     public IActionResult LoadJobPostingsTable()
//     {
//         var jobPostings = _db.JobPostings.ToList();
//         return PartialView("_JobPostingsTable", jobPostings);
//     }
// }
