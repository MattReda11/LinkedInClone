using Microsoft.AspNetCore.Mvc;

namespace LinkedInClone.Controllers
{
    public class JobPostingsController : Controller
    {
        // 
        // GET: /JobPostings/
        public IActionResult Index()
        {
            return View();
        }
        // 
        // GET: /JobPostings/Add/ 
        public IActionResult Add()
        {
            return View();
        }
    }
}