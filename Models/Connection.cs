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

        public IdentityUser SentBy { get; set; }
        public IdentityUser ReceivedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Accepted { get; set; }
    }
}