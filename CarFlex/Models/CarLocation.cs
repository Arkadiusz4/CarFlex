using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models;

public class CarLocation
{
    [Key] public int CarLocationID { get; set; }

    [Required] public int CarID { get; set; }

    [Required] public int LocationID { get; set; }

    public Car Car { get; set; }
    public Location Location { get; set; }
}