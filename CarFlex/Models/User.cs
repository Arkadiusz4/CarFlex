using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFlex.Models
{
    public class User
    {
        [Key] public int Id { get; set; }

        [Required] [StringLength(50)] public string Username { get; set; }

        [NotMapped] [Required] public string Password { get; set; }

        [Required] public string HashedPassword { get; set; }

        [StringLength(50)] public string Role { get; set; }
    }
}
