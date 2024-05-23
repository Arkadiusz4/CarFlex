using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models;

public class Customer
{
    [Key] public int CustomerId { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(15)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(255)]
    [Display(Name = "Address")]
    public string Address { get; set; }

    [Required]
    [StringLength(20)]
    [Display(Name = "Driver's License Number")]
    public string DriversLicenseNumber { get; set; }

    public ICollection<Rental> Rentals { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}