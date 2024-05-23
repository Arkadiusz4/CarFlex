using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models;

public class Rental
{
    [Key] public int RentalId { get; set; }

    [Required] public int CustomerId { get; set; }

    [Required] public int CarId { get; set; }

    [Required]
    [Display(Name = "Rental date")]
    [DataType(DataType.DateTime)]
    public DateTime RentalDate { get; set; }

    [Required]
    [Display(Name = "Return date")]
    [DataType(DataType.DateTime)]
    public DateTime ReturnDate { get; set; }

    [Required]
    [Display(Name = "Total cost")]
    [DataType(DataType.Currency)]
    public decimal TotalCost { get; set; }

    public Customer Customer { get; set; }
    public Car Car { get; set; }
    public ICollection<Payment> Payments { get; set; }
}