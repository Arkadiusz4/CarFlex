using System.ComponentModel.DataAnnotations;

namespace CarFlex.Models;

public class UserEditViewModel
{
    public int Id { get; set; }

    [Microsoft.Build.Framework.Required]
    [StringLength(50)]
    public string Username { get; set; }

    public string Password { get; set; }

    [StringLength(50)] public string Role { get; set; }
}