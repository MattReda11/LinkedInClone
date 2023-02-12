using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class JobApplication
    {
        public int JobApplicationId { get; set; }

        [Required]
        public ApplicationUser Applicant { get; set; }

        [Required]
        public JobPosting Job { get; set; }

#nullable enable
        public string? FilePath { get; set; }

#nullable enable
        public string? FileName { get; set; }

        [Display(Name = "Creation Date"), DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}