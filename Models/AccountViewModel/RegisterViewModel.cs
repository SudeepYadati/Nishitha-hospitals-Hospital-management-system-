using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.AccountViewModel
{
    public class RegisterViewModel
    {
        [Required, StringLength(50)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^(Admin|Patient)$", ErrorMessage = "Invalid Role")]
        public string Role { get; set; }  // Only "Admin" or "Patient"
    }
}