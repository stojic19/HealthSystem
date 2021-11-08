using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Pharmacy.EfStructures
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = Environment.GetEnvironmentVariable("PHARMACY_DB_PATH");

            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }

        public static DbContextOptions<AppDbContext> GetOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = Environment.GetEnvironmentVariable("PHARMACY_DB_PATH");

            optionsBuilder.UseNpgsql(connectionString);

            return optionsBuilder.Options;
        }
    }
}
