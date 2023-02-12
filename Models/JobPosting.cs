using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class JobPosting
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("RecruiterId")]
        public virtual ApplicationUser Recruiter { get; set; }

        [NotMapped]
        public string RecruitId { get; set; }

        public void SetRecruiter(ApplicationUser recruiter)
        {
            Recruiter = recruiter;
            RecruitId = Recruiter.Id;
        }

        [Required]
        [StringLength(200)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Creation Date"), DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<JobApplication> JobApplications { get; set; }
    }
}