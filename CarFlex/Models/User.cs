using Microsoft.AspNetCore.Identity;

namespace CarFlex.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}
