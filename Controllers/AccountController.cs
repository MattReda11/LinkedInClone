using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Authorization;
using LinkedInClone.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LinkedInClone.Controllers;
[Area("Account")]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _db;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;


    public AccountController(ILogger<AccountController> logger, AppDbContext db, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _db = db;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _userManager = userManager;
    }


    [AllowAnonymous]
    public IActionResult GoogleLogin()
    {
        string redirectUrl = Url.Action("GoogleResponse", "Account");
        var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
        return new ChallengeResult("Google", properties);
    }

    [AllowAnonymous]
    public async Task<IActionResult> GoogleResponse()
    {
        ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();

        if (info == null)
            return View("ExternalLoginFailed");

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

        string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
        if (result.Succeeded)
        {
            _logger.LogInformation($"[DEBUG-1]User {userInfo[0]} signed in successfully using google.");
            return RedirectToAction("Index", "Home");
        }
        else
        {
            var user = await _userManager.FindByEmailAsync(info.Principal.FindFirst(ClaimTypes.Email).Value);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                foreach (var error in identResult.Errors)
                {
                    _logger.LogInformation($"Create Async result -> {error.Description}");
                }
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        _logger.LogInformation($"Google login information added for user {userInfo[0]}");
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                var identResult = await _userManager.AddLoginAsync(user, info);
                if (identResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return AccessDenied();
        }
    }

    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            Console.WriteLine($"User {id} not found");
            return RedirectToAction("Home", "AdminPanel");
        }
        else
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Dlt"] = $"User {user.Email} deleted!";
                return RedirectToAction("Home", "AdminPanel");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

        }
        return RedirectToAction("Home", "AdminPanel");
    }

    public async Task<IActionResult> Read(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        Console.WriteLine("Found user: " + user);
        return RedirectToAction("Home", "AdminPanel");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
