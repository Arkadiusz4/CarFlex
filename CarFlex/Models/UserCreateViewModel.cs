using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models
{
    public class UserCreateViewModel
    {
        [Microsoft.Build.Framework.Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Microsoft.Build.Framework.Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(50)] public string Role { get; set; }
    }
}
