namespace ResumeManager.Models
{
    public class LoginResponseDTO
    {
        public LocalUser UserDetails { get; set; }
        public string Token { get; set; }
    }
}
