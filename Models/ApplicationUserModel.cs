using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace HospitalManagementSystem.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        [Required,StringLength(50)]
        public string? FullName { get; set; } 
        [Required]
        [RegularExpression("^(Admin|Patient)$",ErrorMessage = "Invalid Role")]
        public string Role { get; set; }// Role of the user it can be either Admin,Patient
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
   
}


































//public enum ApplicationRole
//{
//    Admin,
//    Patient
//}