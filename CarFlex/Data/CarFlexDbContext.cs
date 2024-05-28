using CarFlex.Models;
using Microsoft.EntityFrameworkCore;

public class CarFlexDbContext : DbContext
{
    public CarFlexDbContext(DbContextOptions<CarFlexDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; } = default!;

    public DbSet<Customer> Customers { get; set; } = default!;

    public DbSet<Location> Locations { get; set; } = default!;

    public DbSet<Rental> Rentals { get; set; } = default!;

    public DbSet<User> Users { get; set; } = default!;
}
