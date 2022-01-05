using Hospital.GraphicalEditor.Model;
using Hospital.MedicalRecords.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Database.EfStructures
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<HospitalTreatment> HospitalTreatments { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationInventory> MedicationInventory { get; set; }
        public DbSet<MedicationIngredient> MedicationIngredients { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomInventory> RoomInventories { get; set; }
        public DbSet<ScheduledEvent> ScheduledEvents { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<AnsweredQuestion> AnsweredQuestions { get; set; }
        public DbSet<AnsweredSurvey> AnsweredSurveys { get; set; }
        public DbSet<EquipmentTransferEvent> EquipmentTransferEvents { get; set; }
        public DbSet<MedicationExpenditureLog> MedicationExpenditureLogs { get; set; }
        public DbSet<RoomPosition> RoomPositions { get; set; }
        public DbSet<RoomRenovationEvent> RoomRenovationEvents { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().OwnsOne(u => u.City, c => c.OwnsOne(x => x.Country));
            modelBuilder.Entity<Doctor>().OwnsOne(d => d.Specialization);
            modelBuilder.Entity<MedicalRecord>().OwnsOne(d => d.Measurements);
            base.OnModelCreating(modelBuilder);
        }
    }
}