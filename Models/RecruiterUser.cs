using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class RecruiterUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Company { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
    }
}