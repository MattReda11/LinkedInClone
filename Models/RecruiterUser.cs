using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class RecruiterUser : ApplicationUser
    {
        [Required]
        [StringLength(100)]
        public new string Company { get; set; }

        [Required]
        [StringLength(100)]
        public new string FullName { get; set; }
    }
}