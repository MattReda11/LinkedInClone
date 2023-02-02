using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser Applicant { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public string FileName { get; set; }

        [Display(Name = "Creation Date"), DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}