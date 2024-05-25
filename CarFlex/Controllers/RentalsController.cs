using CarFlex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarFlex.Controllers
{
    public class RentalsController : Controller
    {
        private readonly CarFlexDbContext _context;

        public RentalsController(CarFlexDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rental.ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalId,CustomerId,CarId,RentalDate,ReturnDate,TotalCost")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                var car = await _context.Car.FirstOrDefaultAsync(c => c.CarId == rental.CarId);
                if (car == null)
                {
                    ModelState.AddModelError("CarId", "Invalid Car ID.");
                    return View(rental);
                }

                var rentalDays = (rental.ReturnDate - rental.RentalDate).TotalDays;
                rental.TotalCost = (decimal)rentalDays * car.RentalPricePerDay;

                _context.Rental.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalId,CustomerId,CarId,RentalDate,ReturnDate,TotalCost")] Rental rental)
        {
            if (id != rental.RentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Calculate the total cost
                    var car = await _context.Car.FirstOrDefaultAsync(c => c.CarId == rental.CarId);
                    if (car == null)
                    {
                        ModelState.AddModelError("CarId", "Invalid Car ID.");
                        return View(rental);
                    }

                    var rentalDays = (rental.ReturnDate - rental.RentalDate).TotalDays;
                    rental.TotalCost = (decimal)rentalDays * car.RentalPricePerDay;

                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalId))
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
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rental.FindAsync(id);
            if (rental != null)
            {
                _context.Rental.Remove(rental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rental.Any(e => e.RentalId == id);
        }
    }
}
