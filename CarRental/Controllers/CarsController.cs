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
        public IActionResult Create(CarModel car, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile is not null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    car.ImagePath = $"/images/{fileName}";
                }

                car.Id = _cars.Any() ? _cars.Max(c => c.Id) + 1 : 1;
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
        public IActionResult Edit(int id, CarModel updatedCar, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var car = _cars.FirstOrDefault(c => c.Id == id);
                if (car is null)
                {
                    return NotFound();
                }

                if (imageFile is not null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    car.ImagePath = $"/images/{fileName}";
                }

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