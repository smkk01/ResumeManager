﻿using CustomersMasterDetail.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CustomersMasterDetail.DTO
{
    public class ApplicantDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }


        [StringLength(10)]
        [Required]
        public string Gender { get; set; }

        [Required]
        [Range(25, 55, ErrorMessage = "Currently, We have no Position Vacant for Your Age")]
        [DisplayName("Age in Years")]
        public int Age { get; set; }


        [Required]
        [StringLength(50)]
        public string Qualification { get; set; }


        [Required]
        [Range(1, 25, ErrorMessage = "Currently, We Have no Positions Vacant for Your Experience")]
        [DisplayName("Total Experience in Years")]
        public int TotalExperience { get; set; }
        

        public string PhotoUrl { get; set; }

        [Display(Name = "Profile Photo")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }
        public virtual List<ExperienceDTO> ExperienceDetail { get; set; }
        public virtual List<SoftwareExperienceDTO> SoftwareExperience { get; set; }
    }
}