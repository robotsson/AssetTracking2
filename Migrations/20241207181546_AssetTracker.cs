using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetTrackerEF.Migrations
{
    /// <inheritdoc />
    public partial class AssetTracker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePurchased = table.Column<DateOnly>(type: "date", nullable: false),
                    Office = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

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
                    { 8, "Motorola", "SEK", new DateOnly(2022, 8, 16), "Phone", "Razr", "Sweden", 6083.33m },
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
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
