namespace CustomersMasterDetail.DTO
{
    public class ExperienceDTO
    {
        public int ExperienceId { get; set; }
        public int ApplicantId { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public int YearsWorked { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
