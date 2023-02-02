using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public Post Post { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDated { get; set; }
    }
}