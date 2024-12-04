using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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
                InitialCatalog = "eftest2_w49",
                TrustServerCertificate = true
            };
        
            OptionsBuilder.UseSqlServer(builder.ConnectionString);
        }
        

    }

}