using CarFlex.Models;
using Microsoft.AspNetCore.Identity;

namespace CarFlex.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(CarFlexDbContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (context.Car.Any())
            {
                Console.WriteLine("Database has been seeded.");
                return;
            }

            context.Car.RemoveRange(context.Car);
            context.Location.RemoveRange(context.Location);
            context.Customer.RemoveRange(context.Customer);
            context.Rental.RemoveRange(context.Rental);

            var cars = new Car[]
            {
                new Car
                {
                    LocationID = 1, Make = "Toyota", Model = "Corolla", Year = 2020, RegistrationNo = "ABC123", RentalPricePerDay = 50,
                    Availability = true
                },
                new Car
                {
                    LocationID = 2, Make = "Honda", Model = "Civic", Year = 2019, RegistrationNo = "DEF456", RentalPricePerDay = 45,
                    Availability = true
                },
                new Car
                {
                    LocationID = 3, Make = "Opel", Model = "Astra", Year = 2023, RegistrationNo = "ABC124", RentalPricePerDay = 60,
                    Availability = true
                },
                new Car
                {
                    LocationID = 4, Make = "BMW", Model = "M4", Year = 2024, RegistrationNo = "FGH414", RentalPricePerDay = 2000,
                    Availability = true
                },
                new Car
                {
                    LocationID = 5, Make = "Ford", Model = "Focus", Year = 2021, RegistrationNo = "GHI789", RentalPricePerDay = 55,
                    Availability = true
                },
                new Car
                {
                    LocationID = 6, Make = "Chevrolet", Model = "Malibu", Year = 2022, RegistrationNo = "JKL012",
                    RentalPricePerDay = 65, Availability = true
                },
                new Car
                {
                    LocationID = 7, Make = "Nissan", Model = "Sentra", Year = 2020, RegistrationNo = "MNO345", RentalPricePerDay = 50,
                    Availability = true
                },
                new Car
                {
                    LocationID = 1, Make = "Hyundai", Model = "Elantra", Year = 2021, RegistrationNo = "PQR678", RentalPricePerDay = 55,
                    Availability = true
                },
                new Car
                {
                    LocationID = 2, Make = "Volkswagen", Model = "Jetta", Year = 2022, RegistrationNo = "STU901",
                    RentalPricePerDay = 60, Availability = true
                },
                new Car
                {
                    LocationID = 3, Make = "Mazda", Model = "3", Year = 2023, RegistrationNo = "VWX234", RentalPricePerDay = 70,
                    Availability = true
                }
            };

            foreach (var car in cars)
            {
                context.Car.Add(car);
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
                context.Customer.Add(customer);
            }

            context.SaveChanges();

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
                context.Location.Add(location);
            }

            context.SaveChanges();

            // Create roles
            string[] roles = new string[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create admin user
            var adminEmail = "admin@admin.com";
            var adminPassword = "Admin@123";
            if (userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                IdentityUser admin = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                IdentityResult result = userManager.CreateAsync(admin, adminPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                }
            }
            
            var userEmail = "user@user.com";
            var userPassword = "User@123";
            if (userManager.FindByEmailAsync(userEmail).Result == null)
            {
                IdentityUser user = new IdentityUser { UserName = userEmail, Email = userEmail };
                IdentityResult result = userManager.CreateAsync(user, userPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }
    }
}
