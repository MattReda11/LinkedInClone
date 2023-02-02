using Microsoft.AspNetCore.Mvc;

namespace LinkedInClone.Controllers
{
    public class JobPostingsController : Controller
    {
        // 
        // GET: /JobPostings/
        public string Index()
        {
            return "This is my default action...";
        }
        // 
        // GET: /JobPostings/Add/ 
        public string Add()
        {
            return "This is the add action method...";
        }
    }
}