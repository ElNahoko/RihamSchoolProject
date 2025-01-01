using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Entities;

namespace CarRental.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly CarRentalContext _context;

        public ReservationsController(CarRentalContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .ToListAsync();

            return View(reservations);
        }

        // GET: Reservations/Create/5
        public async Task<IActionResult> Create(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null || !car.IsAvailable)
            {
                return NotFound("The selected car is not available.");
            }

            ViewBag.Car = car;
            return View(new Reservation { CarId = carId, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) });
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                var car = await _context.Cars.FindAsync(reservation.CarId);
                ViewBag.Car = car;
                return View(reservation);
            }

            var carEntity = await _context.Cars.FindAsync(reservation.CarId);
            if (carEntity == null || !carEntity.IsAvailable)
            {
                return NotFound("The selected car is not available.");
            }

            var days = (reservation.EndDate - reservation.StartDate).TotalDays;
            reservation.TotalCost = Convert.ToDecimal(days) * carEntity.DailyRate;
            reservation.Status = "Confirmed";

            _context.Reservations.Add(reservation);
            carEntity.IsAvailable = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Confirmation), new { id = reservation.Id });
        }

        // GET: Reservations/Confirmation/5
        public async Task<IActionResult> Confirmation(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }
    }
}
