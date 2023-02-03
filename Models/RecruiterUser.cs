using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class RecruiterUser : IdentityUser
    {
        public string Company { get; set; }
        public string Position { get; set; }
    }
}