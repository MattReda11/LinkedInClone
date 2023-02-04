using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LinkedInClone.Data;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Authorization;
using LinkedInClone.Services;
using LinkedInClone.Models.Blobs;
using System.Drawing;
using System.Drawing.Imaging;

namespace LinkedInClone.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;

        private readonly IBlobService _blobService;

        private readonly ILogger<PostsController> _logger;

        public PostsController(AppDbContext context, IBlobService blobService, ILogger<PostsController> logger)
        {
            _context = context;
            _blobService = blobService;
            _logger = logger;
        }

        public async Task<IActionResult> GetMediaFile(string fileName)
        {
            var downloadedData = await _blobService.GetBlobAsync($"https://fsd05regex.blob.core.windows.net/blob-storage/{fileName}");

            if (downloadedData.ContentType.Contains("image/"))
            {
                var image = Image.FromStream(downloadedData.Content);
                var resizedImage = new Bitmap(image, new Size(500, 300));

                using var imageStream = new MemoryStream();
                resizedImage.Save(imageStream, ImageFormat.Jpeg);

                return File(imageStream.ToArray(), downloadedData.ContentType);
            }
            return File(downloadedData.Content, downloadedData.ContentType);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.Include("Author").ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [Authorize]
        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,FileName")] Post post)
        {
            ModelState.Remove("Author");
            ModelState.Remove("PostedDate");

            var userName = User.Identity.Name;
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();

            var today = DateTime.Now;

            post.Author = user;
            post.PostedDate = today;

            if (ModelState.IsValid)
            {

                if (post.FileName != null)
                {
                    post.FilePath = @$"wwwroot/Images/{post.FileName}";
                    await _blobService.UploadFileBlobAsync(post.FilePath, post.FileName);

                    _logger.LogInformation(string.Empty, "File has been uploaded successfully to Blob.");
                }

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [Authorize]
        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,FileName")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Author");
            ModelState.Remove("PostedDate");

            var userName = User.Identity.Name;
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            post.Author = user;

            post.PostedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    var downloadedData = await _blobService.GetBlobAsync($"https://fsd05regex.blob.core.windows.net/blob-storage/{post.FileName}");

                    if (downloadedData == null)
                    {
                        post.FilePath = @$"wwwroot/Images/{post.FileName}";
                        await _blobService.UploadFileBlobAsync(post.FilePath, post.FileName);

                        _logger.LogInformation(string.Empty, $"File for post {post.Id} has been updated and uploaded to Blob.");
                    }

                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }

        [Authorize]
        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [Authorize]
        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'AppDbContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
