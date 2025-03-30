using System;
using System.ComponentModel.DataAnnotations;
namespace HospitalManagementSystem.Models
{
    public class PatientModel
    {
        [Key]
        public int PatientId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required, StringLength(15)]
        public string ContactNumber { get; set; }

        [Required, StringLength(255)]
        public string Address { get; set; }
    }
}