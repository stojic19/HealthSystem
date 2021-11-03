using Integration.Model;
using Microsoft.EntityFrameworkCore;

namespace Integration.EfStructures
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintResponse> ComplaintResponses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
