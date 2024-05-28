# CarFlex

CarFlex is a car rental management application built with ASP.NET Core MVC. It allows users to browse, rent, and manage cars, locations, customers, and rentals. It also provides functionalities for user authentication and role-based access control.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Contributing](#contributing)

## Features

- **User Authentication**: Secure login and registration with role-based access control.
- **Car Management**: Add, edit, delete, and view cars.
- **Customer Management**: Add, edit, delete, and view customers.
- **Location Management**: Add, edit, delete, and view locations.
- **Rental Management**: Rent cars, manage rentals, and calculate rental costs.
- **Car Statistics**: View car rental rankings and earnings.
- **Responsive Design**: User-friendly and responsive interface.

## Technologies Used

- **Backend**: ASP.NET Core MVC, Entity Framework Core
- **Frontend**: Bootstrap, HTML, CSS
- **Database**: SQLite
- **Authentication**: ASP.NET Identity

## Setup and Installation

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 5.0 or later)
- [Node.js](https://nodejs.org/) (for frontend dependencies if needed)
- SQLite

### Steps

1. **Clone the repository**

   ```sh
   git clone https://github.com/your-username/carflex.git
   cd carflex
   ```
   
2. **Setup the database**

Update the appsettings.json file with your SQLite database connection string.
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=carflex.db"
  }
}
```

3. **Run database migrations**
```sh
dotnet ef database update
```

4. **Install dependencies**
```sh
dotnet restore
```

5. **Build and run the application**
```sh
dotnet build
dotnet run
```

6. **Access the application**

   Open your browser and navigate to https://localhost:5001 or http://localhost:5000.

## Usage

### User Roles
   - Admin: Has access to manage users, cars, customers, locations, and rentals.
   - User: Can browse cars and make rentals.

### Car Rental
   - Navigate to the "Cars" section to view available cars.
   - Click "Rent" next to the desired car to make a rental.
   - Fill in the rental details and submit.
   
### Manage Entities
   - Cars: Add, edit, and delete cars from the "Cars" section.
   - Customers: Manage customer information from the "Customers" section.
   - Locations: Add and edit rental locations from the "Locations" section.
   - Rentals: View and manage all rentals from the "Rentals" section.

### Statistics
   - Access "Car Stats" from the navigation menu to view car rental rankings and earnings.
   Project Structure

## Project Structure
```bash
CarFlex/
│
├── Controllers/       # MVC Controllers
│   ├── AccountController.cs
│   ├── CarsController.cs
│   ├── CustomersController.cs
│   ├── LocationsController.cs
│   └── RentalsController.cs
│
├── Models/            # Application Models
│   ├── Car.cs
│   ├── Customer.cs
│   ├── Location.cs
│   ├── Rental.cs
│   └── User.cs
│
├── Views/             # Razor Views
│   ├── Account/
│   ├── Cars/
│   ├── Customers/
│   ├── Locations/
│   ├── Rentals/
│   └── Shared/
│
├── wwwroot/           # Static files
│   ├── css/
│   ├── js/
│   └── lib/
│
├── appsettings.json   # Configuration file
├── Program.cs         # Program entry point
├── Startup.cs         # Application startup
└── CarFlex.csproj     # Project file
```

## Contributing
Contributions are welcome! Please fork the repository and use a feature branch. Pull requests are warmly welcome.

1. Fork the repository.
2. Create your feature branch (git checkout -b feature/YourFeature).
3. Commit your changes (git commit -am 'Add some feature').
4. Push to the branch (git push origin feature/YourFeature).
5. Create a new Pull Request.


![Database diagram](https://github.com/Arkadiusz4/CarFlex/assets/71427558/3b4be576-2862-4d0a-86b0-9fc5b45f084e)
