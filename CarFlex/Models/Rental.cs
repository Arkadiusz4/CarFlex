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
    [DataType(DataType.Date)]
    [DateAfter("RentalDate", ErrorMessage = "Return date must be after rental date.")]
    public DateTime ReturnDate { get; set; }

    [Required]
    [Display(Name = "Total cost")]
    [DataType(DataType.Currency)]
    public decimal TotalCost { get; set; }

    // public ICollection<Customer> Customers { get; set; }
    // public ICollection<Car> Cars { get; set; }
}

public class DateAfterAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateAfterAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var currentValue = (DateTime)value;

        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (property == null)
            throw new ArgumentException("Property with this name not found");

        var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

        if (currentValue <= comparisonValue)
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }
}
