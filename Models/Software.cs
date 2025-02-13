using System.ComponentModel.DataAnnotations;

namespace CustomersMasterDetail.Models
{
    public class Software
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
    }
}
