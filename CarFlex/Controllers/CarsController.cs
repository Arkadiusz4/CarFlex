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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Car.ToListAsync());
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
