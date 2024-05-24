using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models;

public class Location
{
    [Key] 
    public int LocationId { get; set; }

    [Required]
    [StringLength(255)]
    [Display(Name = "Address")]
    public string Address { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "City")]
    public string City { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "State")]
    public string State { get; set; }

    [Required]
    [StringLength(10)]
    [Display(Name = "Zip Code")]
    public string PostalCode { get; set; }
}
