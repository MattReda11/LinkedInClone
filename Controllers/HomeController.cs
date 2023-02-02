using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Authorization;

namespace LinkedInClone.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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
    /*
[HttpPost]
public ActionResult Create(Connection connection)
{
    if (ModelState.IsValid)
    {
        ApplicationUser sender = db.Users.Find(connection.SenderId);
        ApplicationUser receiver = db.Users.Find(connection.ReceiverId);

        Connection newConnection = new Connection
        {
            AccountOwner = sender,
            Friend = receiver,
            CreatedDate = DateTime.Now,
            Accepted = false,
            SenderId = sender.Id,
            ReceiverId = receiver.Id
        };

        db.Connections.Add(newConnection);
        db.SaveChanges();

        return RedirectToAction("Index");
    }

    return View(connection);
}




    */



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
