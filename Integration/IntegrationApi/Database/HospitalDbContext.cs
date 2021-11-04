using Integration.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Database
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Pharmacy> Pharmacies { get; set; }

        public DbSet<Complaint> Complaints { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Pharmacy>().HasData();
        }
    }
}
