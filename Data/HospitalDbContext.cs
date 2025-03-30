using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HospitalManagementSystem.Models;
namespace HospitalManagementSystem.Data
{
    public class HospitalDbContext : IdentityDbContext<ApplicationUserModel>
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<BillingModel> Bills { get; set; }
        public DbSet<ApplicationUserModel> User { get; set; }
    }
}