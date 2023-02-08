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

    
    
  
    [HttpDelete("/Account/DeleteUser/{id}")]
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

    public IActionResult AccessDenied()
    {
        return View();
    }

    public IActionResult LoadUsersTable()
    {
        var users = _db.AppUsers.ToList();
        return PartialView("_UsersTable", users);
    }

    public IActionResult LoadUserPostsTable()
    {
        var posts = _db.Posts.ToList();
        return PartialView("_UserPostsTable", posts);
    }

    public IActionResult LoadJobPostingsTable()
    {
        var jobPostings = _db.JobPostings.ToList();
        return PartialView("_JobPostingsTable", jobPostings);
    }
}
