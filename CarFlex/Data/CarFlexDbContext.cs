using CarFlex.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class CarFlexDbContext : IdentityDbContext<IdentityUser>
{
    public CarFlexDbContext(DbContextOptions<CarFlexDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Car { get; set; } = default!;

    public DbSet<Customer> Customer { get; set; } = default!;

    public DbSet<Location> Location { get; set; } = default!;

    public DbSet<Rental> Rental { get; set; } = default!;
    public DbSet<User> User { get; set; } = default!;
}
