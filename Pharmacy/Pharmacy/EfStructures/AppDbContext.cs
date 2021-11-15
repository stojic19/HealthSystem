using Pharmacy.Model;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.EfStructures
{
    public class AppDbContext : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
