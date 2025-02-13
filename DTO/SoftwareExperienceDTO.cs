namespace CustomersMasterDetail.DTO
{
    public class SoftwareExperienceDTO
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int SoftwareId { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; }
        public bool IsHidden { get; set; } = false;
    }
}
