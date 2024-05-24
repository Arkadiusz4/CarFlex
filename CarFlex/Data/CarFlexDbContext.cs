using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarFlex.Models;

    public class CarFlexDbContext : DbContext
    {
        public CarFlexDbContext (DbContextOptions<CarFlexDbContext> options)
            : base(options)
        {
        }

        public DbSet<CarFlex.Models.Car> Car { get; set; } = default!;

public DbSet<CarFlex.Models.Customer> Customer { get; set; } = default!;

public DbSet<CarFlex.Models.Location> Location { get; set; } = default!;

public DbSet<CarFlex.Models.Rental> Rental { get; set; } = default!;
    }