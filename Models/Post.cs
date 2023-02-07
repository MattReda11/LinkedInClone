using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostedDate { get; set; }

        public string? FilePath { get; set; }

        public string? FileName { get; set; }

        public IList<Like> Likes { get; set; }
        public IList<Comment> Comments { get; set; }

    }
}