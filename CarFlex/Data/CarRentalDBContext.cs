using CarFlex.Models;
using Microsoft.EntityFrameworkCore;

namespace CarFlex.Data;

public class CarRentalDbContext : DbContext
{
    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<CarLocation> CarLocations { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarLocation>()
            .HasOne(cl => cl.Car)
            .WithMany(c => c.CarLocations)
            .HasForeignKey(cl => cl.CarID);

        modelBuilder.Entity<CarLocation>()
            .HasOne(cl => cl.Location)
            .WithMany(l => l.CarLocations)
            .HasForeignKey(cl => cl.LocationID);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CustomerId);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Car)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CarId);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Rental)
            .WithMany(r => r.Payments)
            .HasForeignKey(p => p.RentalId);

        modelBuilder.Entity<Reservation>()
            .HasOne(rs => rs.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(rs => rs.CustomerId);

        modelBuilder.Entity<Reservation>()
            .HasOne(rs => rs.Car)
            .WithMany(c => c.Reservations)
            .HasForeignKey(rs => rs.CarId);

        modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.Car)
            .WithMany(c => c.Maintenances)
            .HasForeignKey(m => m.CarId);
    }
}
