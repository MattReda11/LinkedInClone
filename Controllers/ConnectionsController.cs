using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LinkedInClone.Data;
using LinkedInClone.Models;
using LinkedInClone.Services;

namespace LinkedInClone.Controllers
{
    public class ConnectionsController : Controller
    {
        private readonly AppDbContext _context;

        private readonly INewsAPIService _newsAPIService;

        public ConnectionsController(AppDbContext context, INewsAPIService newsAPIService)
        {
            _context = context;
            _newsAPIService = newsAPIService;
        }

        public async Task<IActionResult> SearchResults(string searchKeyWord)
        {
            // SoundsLike static function used here
            var model = await _context.AppUsers.Where(appUser => AppDbContext.SoundsLike(appUser.FullName) == AppDbContext.SoundsLike(searchKeyWord)).ToListAsync();
            return View(model);
        }

        // GET: Connections
        public async Task<IActionResult> Index()
        {
            //articles
            NewsResponse apiResponse = await _newsAPIService.GetHeadlines();
            var articles = apiResponse.articles;
            ViewBag.Articles = articles;

            //connections
            var username = User.Identity.Name;
            var owner = _context.AppUsers.Where(appUser => appUser.UserName == username).FirstOrDefault();
            return View(await _context.Connections.Include("Friend").Include("AccountOwner").ToListAsync());
        }

        // GET: Connections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Connections == null)
            {
                return NotFound();
            }

            var connection = await _context.Connections.Include("AccountOwner").Include("Friend").FirstOrDefaultAsync(m => m.Id == id);
            if (connection == null)
            {
                return NotFound();
            }

            return View(connection);
        }

        // GET: Connections/Create
        public IActionResult AddConnection(string id)
        {
            var friend = _context.AppUsers.Find(id);
            var username = User.Identity.Name;
            var owner = _context.AppUsers.Where(appUser => appUser.UserName == username).FirstOrDefault();

            var connection = new Connection { AccountOwner = owner, Friend = friend };

            var checkConnection1 = _context.Connections.Where(c => c.AccountOwner == owner && c.Friend == friend).FirstOrDefault();
            var checkConnection2 = _context.Connections.Where(c => c.AccountOwner == friend && c.Friend == owner).FirstOrDefault();

            if (checkConnection1 != null || checkConnection2 != null)
            {
                TempData["generalInfo"] = $"Connection already exists!";
                return RedirectToAction(nameof(Index));
            }

            _context.Connections.Add(connection);
            _context.SaveChanges();

            TempData["generalInfo"] = $"Connection to {friend.FullName} has been Requested!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Connections/Accept/5
        public async Task<IActionResult> Accept(int? id)
        {
            if (id == null || _context.Connections == null)
            {
                return NotFound();
            }

            var connection = await _context.Connections.FindAsync(id);
            connection.Accepted = true;

            _context.Update(connection);
            await _context.SaveChangesAsync();

            TempData["generalInfo"] = $"Connection Accepted!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Deny(int? id)
        {
            if (id == null || _context.Connections == null)
            {
                return NotFound();
            }

            var connection = await _context.Connections.FindAsync(id);

            _context.Connections.Remove(connection);
            await _context.SaveChangesAsync();

            TempData["generalInfo"] = $"Connection has been Denied!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Connections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Connections == null)
            {
                return NotFound();
            }

            var connection = await _context.Connections.Include("AccountOwner").Include("Friend").FirstOrDefaultAsync(m => m.Id == id);
            if (connection == null)
            {
                return NotFound();
            }

            return View(connection);
        }

        // POST: Connections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Connections == null)
            {
                return Problem("Entity set 'AppDbContext.Connections' is null.");
            }
            var connection = await _context.Connections.FindAsync(id);
            if (connection != null)
            {
                _context.Connections.Remove(connection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConnectionExists(int id)
        {
            return _context.Connections.Any(e => e.Id == id);
        }
    }
}
