using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LinkedInClone.Data;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Controllers
{
    public class ConnectionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConnectionsController(AppDbContext context, ILogger<JobPosting> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: Searched Users 
        public async Task<IActionResult> SearchResults(string searchKeyWord)
        {
            // SoundsLike static function used here
            var model = await _context.AppUsers.Where(appUser => AppDbContext.SoundsLike(appUser.FullName) == AppDbContext.SoundsLike(searchKeyWord)).ToListAsync();
            return View(model);
        }

        // GET: Connections
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Connections.Include(c => c.AccountOwner).Include(c => c.Friend);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Connections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Connections == null)
            {
                return NotFound();
            }

            var connection = await _context.Connections
                .Include(c => c.AccountOwner)
                .Include(c => c.Friend)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (connection == null)
            {
                return NotFound();
            }

            return View(connection);
        }

        // GET: Connections/Create
        public IActionResult Create()
        {
            ViewData["SenderId"] = new SelectList(_context.Users, "FullName", "FullName");
            ViewData["ReceiverId"] = new SelectList(_context.Users, "FullName", "FullName");
            return View();
        }

        // POST: Connections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,Accepted,SenderId,ReceiverId")] Connection connection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(connection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SenderId"] = new SelectList(_context.AppUsers, "Id", "Id", connection.SenderId);
            ViewData["ReceiverId"] = new SelectList(_context.AppUsers, "Id", "Id", connection.ReceiverId);
            return View(connection);
        }

        // GET: Connections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Connections == null)
            {
                return NotFound();
            }

            var connection = await _context.Connections.FindAsync(id);
            if (connection == null)
            {
                return NotFound();
            }
            ViewData["SenderId"] = new SelectList(_context.AppUsers, "Id", "Id", connection.SenderId);
            ViewData["ReceiverId"] = new SelectList(_context.AppUsers, "Id", "Id", connection.ReceiverId);
            return View(connection);
        }

        // POST: Connections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDate,Accepted,SenderId,ReceiverId")] Connection connection)
        {
            if (id != connection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(connection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConnectionExists(connection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SenderId"] = new SelectList(_context.AppUsers, "Id", "Id", connection.SenderId);
            ViewData["ReceiverId"] = new SelectList(_context.AppUsers, "Id", "Id", connection.ReceiverId);
            return View(connection);
        }

        // GET: Connections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Connections == null)
            {
                return NotFound();
            }

            var connection = await _context.Connections
                .Include(c => c.AccountOwner)
                .Include(c => c.Friend)
                .FirstOrDefaultAsync(m => m.Id == id);
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
                return Problem("Entity set 'AppDbContext.Connections'  is null.");
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
