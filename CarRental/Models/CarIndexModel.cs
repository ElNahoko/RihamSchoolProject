using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        [Display(Name = "Brand Name")]
        public string Brand { get; set; } = string.Empty;

        [Display(Name = "Model Name")]
        public string Model { get; set; } = string.Empty;

        [Display(Name = "Year of Manufacture")]
        public int Year { get; set; }

        [Display(Name = "Daily Rental Rate")]
        [DataType(DataType.Currency)]
        public decimal DailyRate { get; set; }

        [Display(Name = "Available for Rent")]
        public bool IsAvailable { get; set; }
    }
}