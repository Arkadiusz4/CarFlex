using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models;

public class Car
{
    [Key] public int CarId { get; set; }

    [Required]
    [Display(Name = "Localization")]
    public int LocationID { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Make")]
    public string Make { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Model")]
    public string Model { get; set; }

    [Required]
    [Display(Name = "Year of production")]
    public int Year { get; set; }

    [StringLength(20)]
    [Display(Name = "Registration plate number")]
    public string RegistrationNo { get; set; }

    [Required]
    [Display(Name = "Rental price per day")]
    public decimal RentalPricePerDay { get; set; }
    
    [Display(Name = "Availability")]
    public bool Availability { get; set; }

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    // public ICollection<Location> Locations { get; set; }
    // public virtual Location Location { get; set; }
}
