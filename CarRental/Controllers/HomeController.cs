using Microsoft.AspNetCore.Mvc;
using CarRental.Data;
using CarRental.Entities;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CarRentalContext _context;

        public HomeController(ILogger<HomeController> logger, CarRentalContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars
                .Select(car => new CarModel
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    Year = car.Year,
                    DailyRate = car.DailyRate,
                    IsAvailable = car.IsAvailable,
                    ImagePath = car.ImagePath
                })
                .ToListAsync();

            ViewBag.Discount = "20% off on all rentals!";
            return View(cars);
        }
    }
}