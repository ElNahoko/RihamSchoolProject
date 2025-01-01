using Microsoft.AspNetCore.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Static list to simulate car data
        private static List<CarModel> _cars = new List<CarModel>
        {
            new CarModel { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2019, DailyRate = 50.00m, IsAvailable = true, ImagePath = "/images/toyota_corolla.jpg" },
            new CarModel { Id = 2, Brand = "Honda", Model = "Civic", Year = 2020, DailyRate = 60.00m, IsAvailable = true, ImagePath = "/images/honda_civic.jpg" },
            new CarModel { Id = 3, Brand = "Ford", Model = "Fusion", Year = 2018, DailyRate = 55.00m, IsAvailable = false, ImagePath = "/images/ford_fusion.jpg" }
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Discount = "20% off on all rentals!";
            return View(_cars);
        }
    }
}