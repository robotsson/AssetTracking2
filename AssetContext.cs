using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace AssetTrackerEF
{
   
    internal class AssetTrackerContext : DbContext
    {
        // =null! just to supress annoying warnings
        public DbSet<Asset> Assets { get; set; } = null!;
        public DbSet<Computer> Computer { get; set; } = null!;
        public DbSet<Phone> Phone { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {

            // string ConnectionString = "Data Source=localhost;Initial Catalog=eftest2_w49;User ID=sa;Password=dockerStrongPwd123;Trust Server Certificate=True";

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                UserID = "sa",
                Password = "dockerStrongPwd123",
                InitialCatalog = "AssetTracker",
                TrustServerCertificate = true
            };
            
            string ConnectionString = builder.ConnectionString;        
            // string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=efdb1;Integrated Security=True;";

            OptionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Data Seeding

            modelBuilder.Entity<Computer>().HasData(
                new Computer { Id = 1, Brand = "HP", Model = "Elitebook", Price = 1176.03m, Currency = "EUR", DatePurchased = new DateOnly( 2019, 6, 1 ), Office = "Spain" },
                new Computer { Id = 3, Brand = "Asus", Model = "W234", Price = 4900m, Currency = "SEK", DatePurchased = new DateOnly( 2020, 10, 2 ), Office = "Sweden" },
                new Computer { Id = 4, Brand = "Lenovo", Model = "Yoga 730", Price = 835m, Currency = "USD", DatePurchased = new DateOnly( 2018, 5, 28 ), Office = "USA" },
                new Computer { Id = 7, Brand = "Lenovo", Model = "Yoga 530", Price = 1030m, Currency = "USD", DatePurchased = new DateOnly( 2019, 5, 21 ), Office = "USA" },
                new Computer { Id = 10, Brand = "Apple", Model = "Macbook Pro", Price = 970m, Currency = "EUR", DatePurchased = new DateOnly( 2022, 7, 13 ), Office = "Spain" },
                new Computer { Id = 11, Brand = "Asus", Model = "ROG 500", Price = 9999.90m, Currency = "SEK", DatePurchased = new DateOnly( 2024, 10, 15 ), Office = "Sweden" }
            );

            modelBuilder.Entity<Phone>().HasData(
                new Phone { Id = 2, Brand = "Apple", Model = "Iphone 15", Price = 10000m, Currency = "USD", DatePurchased = new DateOnly( 2024, 9, 11 ), Office = "USA" },
                new Phone { Id = 5, Brand = "Apple", Model = "iPhone", Price = 818.18m, Currency = "EUR", DatePurchased = new DateOnly( 2020, 9, 25 ), Office = "Spain" },
                new Phone { Id = 6, Brand = "Apple", Model = "iPhone", Price = 10375m, Currency = "SEK", DatePurchased = new DateOnly( 2018, 7, 15 ), Office = "Sweden" },
                new Phone { Id = 8, Brand = "Motorola", Model = "Razr", Price = 6083.33m, Currency = "SEK", DatePurchased = new DateOnly( 2022, 5, 16 ), Office = "Sweden" },
                new Phone { Id = 9, Brand = "Samsung", Model = "Galaxy S23", Price = 8083.33m, Currency = "SEK", DatePurchased = new DateOnly( 2023, 3, 16 ), Office = "Sweden" },
                new Phone { Id = 12, Brand = "Nokia", Model = "3310", Price = 160.11m, Currency = "EUR", DatePurchased = new DateOnly( 2019, 5, 16 ), Office = "Germany" },
                new Phone { Id = 13, Brand = "Xiaomi", Model = "14 Ultra", Price = 808.08m, Currency = "EUR", DatePurchased = new DateOnly( 2023, 3, 16 ), Office = "France" } 
            );

        }
    }
}