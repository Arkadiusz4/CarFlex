using CarFlex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarFlex.Controllers
{
    public class RankingController : Controller
    {
        private readonly CarFlexDbContext _context;

        public RankingController(CarFlexDbContext context)
        {
            _context = context;
        }

        // GET: Ranking/CarStats
        public async Task<IActionResult> CarStats(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;

            var carStats = await _context.Rental
                .GroupBy(r => r.CarId)
                .Select(g => new
                {
                    CarId = g.Key,
                    RentalCount = g.Count(),
                    TotalEarnings = g.Sum(r => (double)r.TotalCost)
                })
                .ToListAsync();

            var carStatsViewModel = carStats
                .Join(_context.Car, r => r.CarId, c => c.CarId, (r, c) => new CarStatsViewModel
                {
                    CarId = c.CarId,
                    Make = c.Make,
                    Model = c.Model,
                    RentalCount = r.RentalCount,
                    TotalEarnings = (decimal)r.TotalEarnings
                });

            switch (sortOrder)
            {
                case "carId":
                    carStatsViewModel = carStatsViewModel.OrderBy(c => c.CarId);
                    break;
                case "carId_desc":
                    carStatsViewModel = carStatsViewModel.OrderByDescending(c => c.CarId);
                    break;
                case "rentalCount":
                    carStatsViewModel = carStatsViewModel.OrderBy(c => c.RentalCount);
                    break;
                case "rentalCount_desc":
                    carStatsViewModel = carStatsViewModel.OrderByDescending(c => c.RentalCount);
                    break;
                case "totalEarnings":
                    carStatsViewModel = carStatsViewModel.OrderBy(c => c.TotalEarnings);
                    break;
                case "totalEarnings_desc":
                    carStatsViewModel = carStatsViewModel.OrderByDescending(c => c.TotalEarnings);
                    break;
                default:
                    carStatsViewModel = carStatsViewModel.OrderBy(c => c.CarId);
                    break;
            }

            return View(carStatsViewModel.ToList());
        }
    }
}
