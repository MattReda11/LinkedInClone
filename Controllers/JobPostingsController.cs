using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkedInClone.Data;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Controllers
{
    public class JobPostingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobPostingsController(AppDbContext context, ILogger<JobPosting> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: JobPostings
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobPosting.ToListAsync());
        }

        // GET: JobPostings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobPosting == null)
            {
                return NotFound();
            }

            var jobPosting = await _context.JobPosting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPosting == null)
            {
                return NotFound();
            }

            return View(jobPosting);
        }

        // GET: JobPostings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobPostings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobTitle,Description,CreatedDate")] JobPosting jobPosting)
        {
            ModelState.Remove("Recruiter");
            ApplicationUser loggedInUser = await _userManager.GetUserAsync(User);
            jobPosting.Recruiter = loggedInUser;

            if (ModelState.IsValid)
            {
                _context.Add(jobPosting);
                await _context.SaveChangesAsync();
                _logger.LogInformation("JobPosting added successfully");
                return RedirectToAction(nameof(Index));
            }
            return View(jobPosting);
        }

        // GET: JobPostings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobPosting == null)
            {
                return NotFound();
            }

            var jobPosting = await _context.JobPosting.FindAsync(id);
            if (jobPosting == null)
            {
                return NotFound();
            }
            return View(jobPosting);
        }

        // POST: JobPostings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobTitle,Description,CreatedDate")] JobPosting jobPosting)
        {
            if (id != jobPosting.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Recruiter");
            ApplicationUser loggedInUser = await _userManager.GetUserAsync(User);
            jobPosting.Recruiter = loggedInUser;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPosting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostingExists(jobPosting.Id))
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
            return View(jobPosting);
        }

        // GET: JobPostings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobPosting == null)
            {
                return NotFound();
            }

            var jobPosting = await _context.JobPosting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPosting == null)
            {
                return NotFound();
            }

            return View(jobPosting);
        }

        // POST: JobPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobPosting == null)
            {
                return Problem("Entity set 'AppDbContext.JobPosting'  is null.");
            }
            var jobPosting = await _context.JobPosting.FindAsync(id);
            if (jobPosting != null)
            {
                _context.JobPosting.Remove(jobPosting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostingExists(int id)
        {
            return _context.JobPosting.Any(e => e.Id == id);
        }
    }
}
