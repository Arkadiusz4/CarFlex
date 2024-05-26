using CarFlex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarFlex.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarFlexDbContext _context;

        public CarsController(CarFlexDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Car.ToListAsync());
        // }
        public async Task<IActionResult> Index(string makeFilter, string modelFilter, int? yearFilter, bool? availabilityFilter, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CarIdSortParm = String.IsNullOrEmpty(sortOrder) ? "carId_desc" : "";
            ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            var makes = _context.Car.Select(c => c.Make).Distinct().ToList();
            var models = _context.Car.Select(c => c.Model).Distinct().ToList();
            var years = _context.Car.Select(c => c.Year).Distinct().ToList();

            ViewData["MakeFilter"] = new SelectList(makes);
            ViewData["ModelFilter"] = new SelectList(models);
            ViewData["YearFilter"] = new SelectList(years);
            ViewData["AvailabilityFilter"] = new SelectList(new[] { true, false });

            var cars = from c in _context.Car.Include(c => c.Location)
                select c;

            if (!string.IsNullOrEmpty(makeFilter))
            {
                cars = cars.Where(c => c.Make == makeFilter);
            }

            if (!string.IsNullOrEmpty(modelFilter))
            {
                cars = cars.Where(c => c.Model == modelFilter);
            }

            if (yearFilter.HasValue)
            {
                cars = cars.Where(c => c.Year == yearFilter);
            }

            if (availabilityFilter.HasValue)
            {
                cars = cars.Where(c => c.Availability == availabilityFilter);
            }

            switch (sortOrder)
            {
                case "carId_desc":
                    cars = cars.OrderByDescending(c => c.CarId);
                    break;
                case "Year":
                    cars = cars.OrderBy(c => c.Year);
                    break;
                case "year_desc":
                    cars = cars.OrderByDescending(c => c.Year);
                    break;
                case "Price":
                    cars = cars.OrderBy(c => (double)c.RentalPricePerDay);
                    break;
                case "price_desc":
                    cars = cars.OrderByDescending(c => (double)c.RentalPricePerDay);
                    break;
                default:
                    cars = cars.OrderBy(c => c.CarId);
                    break;
            }

            return View(await cars.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            var locations = _context.Location.Select(l => new 
            {
                LocationId = l.LocationId,
                Display = l.LocationId + " - " + l.Address + ", " + l.City
            }).ToList();
            ViewData["LocationID"] = new SelectList(locations, "LocationId", "Display");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("CarId,LocationID,Make,Model,Year,RegistrationNo,RentalPricePerDay,Availability")]
            Car car)
        {
            car.Rentals = new List<Rental>(); // Assuming Rentals is a collection
            
            if (ModelState.IsValid)
            {
                _context.Car.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            var locations = _context.Location.Select(l => new 
            {
                LocationId = l.LocationId,
                Display = l.LocationId + " - " + l.Address + ", " + l.City
            }).ToList();
            ViewData["LocationID"] = new SelectList(locations, "LocationId", "Display", car.LocationID);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var locations = _context.Location.Select(l => new 
            {
                LocationId = l.LocationId,
                Display = l.LocationId + " - " + l.Address + ", " + l.City
            }).ToList();
            ViewData["LocationID"] = new SelectList(locations, "LocationId", "Display", car.LocationID);

            return View(car);        
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("CarId,LocationID,Make,Model,Year,RegistrationNo,RentalPricePerDay,Availability")]
            Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            var locations = _context.Location.Select(l => new 
            {
                LocationId = l.LocationId,
                Display = l.LocationId + " - " + l.Address + ", " + l.City
            }).ToList();
            ViewData["LocationID"] = new SelectList(locations, "LocationId", "Display", car.LocationID);

            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.CarId == id);
        }

        // GET: Cars/Rent/5
        public async Task<IActionResult> Rent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            car.Availability = false;

            try
            {
                _context.Update(car);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(car.CarId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
