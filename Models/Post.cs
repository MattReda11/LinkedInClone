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
        public IdentityUser Author { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostedDate { get; set; }

        [Required]
        public BlobInfo Image { get; set; }

    }
}