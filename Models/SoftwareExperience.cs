using System.ComponentModel.DataAnnotations.Schema;

namespace CustomersMasterDetail.Models
{
    public class SoftwareExperience
    {
        public int Id { get; set; }
       
        public int ApplicantId { get; set; }
        public virtual Applicant? Applicant { get; private set; }
       
        public int SoftwareId { get; set; }
        public virtual Software? Softwares { get; private set; }

        public int Rating { get; set; }


        public string Notes { get; set; }

        public bool IsHidden { get; set; } = false;
    }
}
