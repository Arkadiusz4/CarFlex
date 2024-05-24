using CarFlex.Models;
using System;
using System.Linq;

namespace CarFlex.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CarFlexDbContext context)
        {
            Console.WriteLine("Ensuring database is created...");
            context.Database.EnsureCreated();

            Console.WriteLine("Checking if database has been seeded...");
            if (context.Car.Any())
            {
                Console.WriteLine("Database has been seeded.");
                return;   // Database has been seeded.
            }

            Console.WriteLine("Seeding Cars...");
            var cars = new Car[]
            {
                new Car { Make = "Toyota", Model = "Corolla", Year = 2020, RegistrationNo = "ABC123", RentalPricePerDay = 50, Availability = true },
                new Car { Make = "Honda", Model = "Civic", Year = 2019, RegistrationNo = "DEF456", RentalPricePerDay = 45, Availability = true },
                new Car { Make = "Opel", Model = "Astra", Year = 2023, RegistrationNo = "ABC124", RentalPricePerDay = 60, Availability = true },
            };

            foreach (var car in cars)
            {
                context.Car.Add(car);
            }
            context.SaveChanges();
            Console.WriteLine("Cars seeded.");

            // Seed Customers
            var customers = new Customer[]
            {
                new Customer { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123456789", Address = "123 Main St", DriversLicenseNumber = "D1234567" },
                new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "987654321", Address = "456 Elm St", DriversLicenseNumber = "D7654321" }
                // Add more customer instances
            };

            foreach (var customer in customers)
            {
                context.Customer.Add(customer);
            }
            context.SaveChanges();

            // Seed Locations
            var locations = new Location[]
            {
                new Location { Address = "123 Car St", City = "CityA", State = "StateA", PostalCode = "12345" },
                new Location { Address = "456 Auto Blvd", City = "CityB", State = "StateB", PostalCode = "67890" }
                // Add more location instances
            };

            foreach (var location in locations)
            {
                context.Location.Add(location);
            }
            context.SaveChanges();

            // Seed additional entities similarly...
        }
    }
}
