using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models;

public class Payment
{
    [Key] public int PaymentId { get; set; }

    [Required] public int RentalId { get; set; }

    [Required]
    [Display(Name = "Payment date")]
    [DataType(DataType.DateTime)]
    public DateTime PaymentDate { get; set; }

    [Required]
    [Display(Name = "Amount")]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Payment method")]
    public string PaymentMethod { get; set; }

    public Rental Rental { get; set; }
}