using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class AdminPanelViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<JobPosting> JobPostings { get; set; }
        public List<Post> Posts { get; set; }
        //public List<Comment> Comments { get; set; }
    }
}