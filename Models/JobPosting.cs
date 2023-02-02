using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class JobPostings
    {
        public int Id { get; set; }

        [Required]
        public IdentityUser Recruiter { get; set; }

        [Required]
        [StringLength(200)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Creation Date"), DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}