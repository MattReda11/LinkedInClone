using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    // connections are when users 'add each other'
    public class Connection
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Account Owner")]
        public IdentityUser AccountOwner { get; set; }

        [Required]
        public IdentityUser Friend { get; set; }

        [Display(Name = "Creation Date"), DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool Accepted { get; set; } = false;
    }
}