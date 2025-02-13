using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomersMasterDetail.Models
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        [ForeignKey("Applicant")]
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }

        [Range(1, 25, ErrorMessage = "Years Must be between 1 an 25")]
        [Required]
        public int YearsWorked { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; } = false;
    }
}
