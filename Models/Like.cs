using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class Like
    {
        public int Id { get; set; }

        [Required]
        public Post LikedPost { get; set; }

        [Required]
        public IdentityUser LikedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LikedDate { get; set; }
    }
}