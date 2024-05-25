using CarFlex.Models;
using Microsoft.AspNetCore.Identity;

namespace CarFlex.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(CarFlexDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
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
                new Car { Make = "Toyota", Model = "Corolla", Year = 2020, RegistrationNo = "ABC123", RentalPricePerDay = 50, Availability = true },
                new Car { Make = "Honda", Model = "Civic", Year = 2019, RegistrationNo = "DEF456", RentalPricePerDay = 45, Availability = true },
                new Car { Make = "Opel", Model = "Astra", Year = 2023, RegistrationNo = "ABC124", RentalPricePerDay = 60, Availability = true },
                new Car { Make = "BMW", Model = "M4", Year = 2024, RegistrationNo = "HWDP", RentalPricePerDay = 2000, Availability = true },
            };

            foreach (var car in cars)
            {
                context.Car.Add(car);
            }

            context.SaveChanges();
            Console.WriteLine("Cars seeded.");

            var customers = new Customer[]
            {
                new Customer { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123456789", Address = "123 Main St", DriversLicenseNumber = "D1234567" },
                new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "987654321", Address = "456 Elm St", DriversLicenseNumber = "D7654321" }
            };

            foreach (var customer in customers)
            {
                context.Customer.Add(customer);
            }

            context.SaveChanges();

            var locations = new Location[]
            {
                new Location { Address = "123 Car St", City = "CityA", State = "StateA", PostalCode = "12345" },
                new Location { Address = "456 Auto Blvd", City = "CityB", State = "StateB", PostalCode = "67890" }
            };

            foreach (var location in locations)
            {
                context.Location.Add(location);
            }

            context.SaveChanges();

            // Seed admin role and user
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (userManager.Users.All(u => u.UserName != "admin"))
            {
                var adminUser = new IdentityUser { UserName = "admin", Email = "admin@example.com" };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
