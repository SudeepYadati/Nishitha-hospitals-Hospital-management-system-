using HospitalManagementSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BillingModel
{
    [Key]
    public int BillId { get; set; }

    [Required]
    [ForeignKey("Patient")]
    public int PatientId { get; set; }
    public PatientModel Patient { get; set; } //  Links to Patient

    [Required]
    public decimal TotalAmount { get; set; }

    [Required]
    public string PaymentStatus { get; set; } // ENUM: PAID, UNPAID

    [Required]
    public DateTime BillDate { get; set; } = DateTime.Now;
}