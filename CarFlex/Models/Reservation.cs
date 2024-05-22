using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models;

public class Reservation
{
    [Key] public int ReservationId { get; set; }

    [Required] public int CustomerId { get; set; }

    [Required] public int CarId { get; set; }

    [Required]
    [Display(Name = "Reservation date")]
    [DataType(DataType.DateTime)]
    public DateTime ReservationDate { get; set; }

    [Required]
    [Display(Name = "Pickup date")]
    [DataType(DataType.DateTime)]
    public DateTime PickupDate { get; set; }

    [Required]
    [Display(Name = "Return date")]
    [DataType(DataType.DateTime)]
    public DateTime ReturnDate { get; set; }

    [Required]
    [StringLength(20)]
    [Display(Name = "Status")]
    public string Status { get; set; }

    public Customer Customer { get; set; }
    public Car Car { get; set; }
}