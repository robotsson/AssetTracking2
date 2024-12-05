using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetTrackerEF.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Brand", "Currency", "DatePurchased", "Discriminator", "Model", "Office", "Price" },
                values: new object[,]
                {
                    { 1, "HP", "EUR", new DateOnly(2019, 6, 1), "Computer", "Elitebook", "Spain", 1176.03m },
                    { 2, "Apple", "USD", new DateOnly(2024, 9, 11), "Phone", "Iphone 15", "USA", 10000m },
                    { 3, "Asus", "SEK", new DateOnly(2020, 10, 2), "Computer", "W234", "Sweden", 4900m },
                    { 4, "Lenovo", "USD", new DateOnly(2018, 5, 28), "Computer", "Yoga 730", "USA", 835m },
                    { 5, "Apple", "EUR", new DateOnly(2020, 9, 25), "Phone", "iPhone", "Spain", 818.18m },
                    { 6, "Apple", "SEK", new DateOnly(2018, 7, 15), "Phone", "iPhone", "Sweden", 10375m },
                    { 7, "Lenovo", "USD", new DateOnly(2019, 5, 21), "Computer", "Yoga 530", "USA", 1030m },
                    { 8, "Motorola", "SEK", new DateOnly(2022, 5, 16), "Phone", "Razr", "Sweden", 6083.33m },
                    { 9, "Samsung", "SEK", new DateOnly(2023, 3, 16), "Phone", "Galaxy S23", "Sweden", 8083.33m },
                    { 10, "Apple", "EUR", new DateOnly(2022, 7, 13), "Computer", "Macbook Pro", "Spain", 970m },
                    { 11, "Asus", "SEK", new DateOnly(2024, 10, 15), "Computer", "ROG 500", "Sweden", 9999.90m },
                    { 12, "Nokia", "EUR", new DateOnly(2019, 5, 16), "Phone", "3310", "Germany", 160.11m },
                    { 13, "Xiaomi", "EUR", new DateOnly(2023, 3, 16), "Phone", "14 Ultra", "France", 808.08m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
