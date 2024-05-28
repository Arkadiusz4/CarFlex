using System;
using System.Linq;
using System.Threading.Tasks;
using CarFlex.Models;
using CarFlex.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace CarFlex.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(CarFlexDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cars.Any())
            {
                Console.WriteLine("Database has been seeded.");
                return;
            }

            context.Cars.RemoveRange(context.Cars);
            context.Locations.RemoveRange(context.Locations);
            context.Customers.RemoveRange(context.Customers);
            context.Rentals.RemoveRange(context.Rentals);
            context.Users.RemoveRange(context.Users);

            var locations = new Location[]
            {
                new Location { Address = "123 Car St", City = "Krakow", State = "StateA", PostalCode = "12345" },
                new Location { Address = "456 Auto Blvd", City = "Krakow", State = "StateA", PostalCode = "12346" },
                new Location { Address = "789 Motor Ave", City = "Krakow", State = "StateA", PostalCode = "12347" },
                new Location { Address = "321 Drive Rd", City = "Warsaw", State = "StateB", PostalCode = "23456" },
                new Location { Address = "654 Road St", City = "Warsaw", State = "StateB", PostalCode = "23457" },
                new Location { Address = "987 Wheel Ln", City = "Gdansk", State = "StateC", PostalCode = "34567" },
                new Location { Address = "123 Route Blvd", City = "Gdansk", State = "StateC", PostalCode = "34568" }
            };

            foreach (var location in locations)
            {
                context.Locations.Add(location);
            }

            context.SaveChanges();
            Console.WriteLine("Locations seeded.");

            var cars = new Car[]
            {
                new Car
                {
                    LocationID = locations[0].LocationId, Make = "Toyota", Model = "Corolla", Year = 2020,
                    RegistrationNo = "ABC123",
                    RentalPricePerDay = 50, Availability = true
                },
                new Car
                {
                    LocationID = locations[1].LocationId, Make = "Honda", Model = "Civic", Year = 2019,
                    RegistrationNo = "DEF456",
                    RentalPricePerDay = 45, Availability = true
                },
                new Car
                {
                    LocationID = locations[2].LocationId, Make = "Opel", Model = "Astra", Year = 2023,
                    RegistrationNo = "ABC124",
                    RentalPricePerDay = 60, Availability = true
                },
                new Car
                {
                    LocationID = locations[3].LocationId, Make = "BMW", Model = "M4", Year = 2024,
                    RegistrationNo = "FGH414",
                    RentalPricePerDay = 2000, Availability = true
                },
                new Car
                {
                    LocationID = locations[4].LocationId, Make = "Ford", Model = "Focus", Year = 2021,
                    RegistrationNo = "GHI789",
                    RentalPricePerDay = 55, Availability = true
                },
                new Car
                {
                    LocationID = locations[5].LocationId, Make = "Chevrolet", Model = "Malibu", Year = 2022,
                    RegistrationNo = "JKL012",
                    RentalPricePerDay = 65, Availability = true
                },
                new Car
                {
                    LocationID = locations[6].LocationId, Make = "Nissan", Model = "Sentra", Year = 2020,
                    RegistrationNo = "MNO345",
                    RentalPricePerDay = 50, Availability = true
                },
                new Car
                {
                    LocationID = locations[0].LocationId, Make = "Hyundai", Model = "Elantra", Year = 2021,
                    RegistrationNo = "PQR678",
                    RentalPricePerDay = 55, Availability = true
                },
                new Car
                {
                    LocationID = locations[1].LocationId, Make = "Volkswagen", Model = "Jetta", Year = 2022,
                    RegistrationNo = "STU901",
                    RentalPricePerDay = 60, Availability = true
                },
                new Car
                {
                    LocationID = locations[2].LocationId, Make = "Mazda", Model = "3", Year = 2023,
                    RegistrationNo = "VWX234",
                    RentalPricePerDay = 70, Availability = true
                }
            };

            foreach (var car in cars)
            {
                context.Cars.Add(car);
            }

            context.SaveChanges();
            Console.WriteLine("Cars seeded.");

            var customers = new Customer[]
            {
                new Customer
                {
                    FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123456789",
                    Address = "123 Main St", DriversLicenseNumber = "D1234567"
                },
                new Customer
                {
                    FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "987654321",
                    Address = "456 Elm St", DriversLicenseNumber = "D7654321"
                }
            };

            foreach (var customer in customers)
            {
                context.Customers.Add(customer);
            }

            context.SaveChanges();

            var admin = new User
            {
                Username = "admin",
                HashedPassword = PasswordHasher.HashPassword("admin123"),
                Role = "Admin"
            };

            context.Users.Add(admin);
            context.SaveChanges();
            Console.WriteLine("Admin user seeded.");

            var user = new User
            {
                Username = "user",
                HashedPassword = PasswordHasher.HashPassword("user123"),
                Role = "User"
            };

            context.Users.Add(user);
            context.SaveChanges();
            Console.WriteLine("User user seeded.");
        }
    }
}