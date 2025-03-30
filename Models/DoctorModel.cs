using System.ComponentModel.DataAnnotations;
namespace HospitalManagementSystem.Models
{



    public class DoctorModel
    {
        [Key]
        public int DoctorId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Specialization { get; set; }

        [Required, StringLength(15)]
        public string ContactNumber { get; set; }

        [Required, StringLength(255)]
        public string AvailabilitySchedule { get; set; } 
    }
}