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
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<MedicineReportFile> MedicineReportFiles { get; set; }
        public DbSet<TenderOffer> TenderOffers { get; set; }
        public DbSet<Tender> Tenders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tender>().OwnsMany(t => t.MedicationRequests);
            modelBuilder.Entity<Tender>().OwnsOne(t => t.ActiveRange);
            modelBuilder.Entity<TenderOffer>().OwnsOne(t => t.Cost);
            modelBuilder.Entity<TenderOffer>().OwnsMany(t => t.MedicationRequests);
            base.OnModelCreating(modelBuilder);
        }
    }
}
