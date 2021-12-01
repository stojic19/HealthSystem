﻿using Integration.Partnership.Model;
using Integration.Pharmacies.Model;
using Integration.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Integration.Database.EfStructures
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintResponse> ComplaintResponses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<MedicineSpecificationFile> MedicineSpecificationFiles { get; set; }
        public DbSet<MedicineInventory> MedicineInventory { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}