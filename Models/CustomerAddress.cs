using System.ComponentModel.DataAnnotations;

namespace CustomersMasterDetail.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
