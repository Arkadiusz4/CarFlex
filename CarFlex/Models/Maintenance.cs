using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models;

public class Maintenance
{
    [Key] public int MaintenanceId { get; set; }

    [Required] public int CarId { get; set; }

    [Required]
    [Display(Name = "Maintenance Date")]
    [DataType(DataType.DateTime)]
    public DateTime MaintenanceDate { get; set; }

    [Required]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Required]
    [Display(Name = "Cost")]
    [DataType(DataType.Currency)]
    public decimal Cost { get; set; }

    public Car Car { get; set; }
}