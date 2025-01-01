using Microsoft.AspNetCore.Mvc;
using CarRental.Data;
using CarRental.Entities;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRentalContext _context;

        public CarsController(CarRentalContext context)
        {
            _context = context;
        }

        // GET: Cars/Index
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

            return View(cars);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var car = await _context.Cars
                .Where(c => c.Id == id)
                .Select(c => new CarModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    DailyRate = c.DailyRate,
                    IsAvailable = c.IsAvailable,
                    ImagePath = c.ImagePath
                })
                .FirstOrDefaultAsync();

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
        public async Task<IActionResult> Create(CarModel carModel, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var car = new Car
                {
                    Brand = carModel.Brand,
                    Model = carModel.Model,
                    Year = carModel.Year,
                    DailyRate = carModel.DailyRate,
                    IsAvailable = carModel.IsAvailable
                };

                if (imageFile != null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    car.ImagePath = $"/images/{fileName}";
                }

                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(carModel);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var carModel = new CarModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                DailyRate = car.DailyRate,
                IsAvailable = car.IsAvailable,
                ImagePath = car.ImagePath
            };

            return View(carModel);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarModel carModel, IFormFile? imageFile)
        {
            if (id != carModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var car = await _context.Cars.FindAsync(id);
                if (car == null)
                {
                    return NotFound();
                }

                car.Brand = carModel.Brand;
                car.Model = carModel.Model;
                car.Year = carModel.Year;
                car.DailyRate = carModel.DailyRate;
                car.IsAvailable = carModel.IsAvailable;

                if (imageFile != null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    car.ImagePath = $"/images/{fileName}";
                }

                _context.Update(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(carModel);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _context.Cars
                .Where(c => c.Id == id)
                .Select(c => new CarModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    DailyRate = c.DailyRate,
                    IsAvailable = c.IsAvailable,
                    ImagePath = c.ImagePath
                })
                .FirstOrDefaultAsync();

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
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}