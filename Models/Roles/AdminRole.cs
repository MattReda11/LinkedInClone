using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models.Roles
{
    public class AdminRole : IdentityRole
    {
        public AdminRole() : base("Admin")
        {
        }
    }
}