using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInClone.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        // [Required]
        // public 
    }
}