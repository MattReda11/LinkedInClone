using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkedInClone.Data;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LinkedInClone.Controllers
{
    [Authorize(Roles = "User")]
    public class JobApplicationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobApplicationsController(AppDbContext context, ILogger<JobPosting> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: All Job Applications for logged in user only
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobApplications.Include("Job").Where(ja => ja.Applicant.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).ToListAsync());
        }

        // GET: All JobPostings
        public async Task<IActionResult> AllJobs()
        {
            return View(await _context.JobPostings.ToListAsync());
        }

        // GET: JobApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobApplications == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .FirstOrDefaultAsync(m => m.JobApplicationId == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // GET: JobApplications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobApplications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,FilePath,FileName,CreatedDate")] JobApplication jobApplication)
        {
            ModelState.Remove("Applicant");
            ModelState.Remove("Job");
            jobApplication.Applicant = await _userManager.GetUserAsync(User);
            jobApplication.Job = await _context.JobPostings.Where(jp => jp.Id == id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                _context.Add(jobApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobApplication);
        }

        // GET: JobApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobApplications == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            return View(jobApplication);
        }

        // POST: JobApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FilePath,FileName,CreatedDate")] JobApplication jobApplication)
        {
            if (id != jobApplication.JobApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobApplicationExists(jobApplication.JobApplicationId))
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
            return View(jobApplication);
        }

        // GET: JobApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobApplications == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .FirstOrDefaultAsync(m => m.JobApplicationId == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // POST: JobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobApplications == null)
            {
                return Problem("Entity set 'AppDbContext.JobApplications'  is null.");
            }
            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication != null)
            {
                _context.JobApplications.Remove(jobApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobApplicationExists(int id)
        {
            return _context.JobApplications.Any(e => e.JobApplicationId == id);
        }
    }
}
