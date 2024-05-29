using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models
{
    public class UserCreateViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        // Fields for Customer
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
    }
}