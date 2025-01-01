using Microsoft.EntityFrameworkCore;
using CarRental.Entities;

namespace CarRental.Data
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options)
            : base(options)
        {
        }

        public required DbSet<Car> Cars { get; set; }
        public required DbSet<Customer> Customers { get; set; }
        public required DbSet<Reservation> Reservations { get; set; }
        public required DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Admin Configuration
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Email)
                .IsUnique();

            // Car Configuration
            modelBuilder.Entity<Car>()
                .Property(c => c.DailyRate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Car>()
                .Property(c => c.IsAvailable)
                .HasDefaultValue(true);

            // Reservation Configuration
            modelBuilder.Entity<Reservation>()
                .Property(r => r.TotalCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Reservation>()
                .Property(r => r.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            // Configure relationships
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2019, DailyRate = 50.00m, IsAvailable = true, ImagePath = "/images/toyota_corolla.jpg" },
                new Car { Id = 2, Brand = "Honda", Model = "Civic", Year = 2020, DailyRate = 60.00m, IsAvailable = true, ImagePath = "/images/honda_civic.jpg" },
                new Car { Id = 3, Brand = "Ford", Model = "Fusion", Year = 2018, DailyRate = 55.00m, IsAvailable = false, ImagePath = "/images/ford_fusion.jpg" }
                );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Jhon Doe", Email = "john.doe@example.com" },
                new Customer { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    CustomerId = 1,
                    CarId = 1,
                    StartDate = new DateTime(2023, 11, 1),
                    EndDate = new DateTime(2023, 11, 5),
                    TotalCost = 200.00m,
                    Status = "Confirmed"
                },
                new Reservation
                {
                    Id = 2,
                    CustomerId = 2,
                    CarId = 2,
                    StartDate = new DateTime(2023, 11, 10),
                    EndDate = new DateTime(2023, 11, 12),
                    TotalCost = 120.00m,
                    Status = "Pending"
                }
            );
        }
    }
}