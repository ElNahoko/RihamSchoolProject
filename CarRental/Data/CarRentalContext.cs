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
        }
    }
}