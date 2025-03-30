using HospitalManagementSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AppointmentModel
{
    [Key]
    public int AppointmentId { get; set; }

    [Required]
    [ForeignKey("Patient")]
    public int PatientId { get; set; }
    public PatientModel Patient { get; set; } //  Establishes relation with Patient

    [Required]
    [ForeignKey("Doctor")]
    public int DoctorId { get; set; }
    public DoctorModel Doctor { get; set; } //  Establishes relation with Doctor

    [Required]
    public DateTime AppointmentDate { get; set; }

    [Required, StringLength(20)]
    public string TimeSlot { get; set; }

    [Required]
    public string Status { get; set; } //  CONFIRMED, CANCELLED
}