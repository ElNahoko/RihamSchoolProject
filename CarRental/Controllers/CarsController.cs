using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CarsController : Controller
    {
        private static List<CarModel> _cars = new List<CarModel>
        {
            new CarModel { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2019, DailyRate = 50.00m, IsAvailable = true },
            new CarModel { Id = 2, Brand = "Honda", Model = "Civic", Year = 2020, DailyRate = 60.00m, IsAvailable = true },
            new CarModel { Id = 3, Brand = "Ford", Model = "Fusion", Year = 2018, DailyRate = 55.00m, IsAvailable = false }
        };

        // GET: Cars/Index
        public IActionResult Index()
        {
            return View(_cars);
        }

        // GET: Cars/Details/5
        public IActionResult Details(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarModel car)
        {
            if (ModelState.IsValid)
            {
                car.Id = _cars.Any() ? _cars.Max(c => c.Id) + 1 : 1; // Assign a new ID
                _cars.Add(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public IActionResult Edit(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CarModel updatedCar)
        {
            if (ModelState.IsValid)
            {
                var car = _cars.FirstOrDefault(c => c.Id == id);
                if (car == null)
                {
                    return NotFound();
                }

                // Update car details
                car.Brand = updatedCar.Brand;
                car.Model = updatedCar.Model;
                car.Year = updatedCar.Year;
                car.DailyRate = updatedCar.DailyRate;
                car.IsAvailable = updatedCar.IsAvailable;

                return RedirectToAction(nameof(Index));
            }
            return View(updatedCar);
        }

        // GET: Cars/Delete/5
        public IActionResult Delete(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                _cars.Remove(car);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}