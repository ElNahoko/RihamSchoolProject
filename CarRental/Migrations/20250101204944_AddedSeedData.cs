using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "DailyRate", "ImagePath", "IsAvailable", "Model", "Year" },
                values: new object[,]
                {
                    { 1, "Toyota", 50.00m, "/images/toyota_corolla.jpg", true, "Corolla", 2019 },
                    { 2, "Honda", 60.00m, "/images/honda_civic.jpg", true, "Civic", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "DailyRate", "ImagePath", "Model", "Year" },
                values: new object[] { 3, "Ford", 55.00m, "/images/ford_fusion.jpg", "Fusion", 2018 });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "", "john.doe@example.com", "Jhon Doe", "" },
                    { 2, "", "jane.smith@example.com", "Jane Smith", "" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CarId", "CustomerId", "EndDate", "StartDate", "Status", "TotalCost" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Confirmed", 200.00m },
                    { 2, 2, 2, new DateTime(2023, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 120.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
