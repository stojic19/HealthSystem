using Pharmacy.Model;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.EfStructures
{
    public class AppDbContext : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintResponse> ComplaintResponses { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Substance> Substances { get; set; }
        public DbSet<MedicineCombination> MedicineCombinations { get; set; }
        public DbSet<MedicineReportFile> MedicineReportFiles { get; set; }
        public DbSet<SideEffect> SideEffects { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Precaution> Precautions { get; set; }
        public DbSet<MedicinePotentialDanger> MedicinePotentialDangers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    }
}
