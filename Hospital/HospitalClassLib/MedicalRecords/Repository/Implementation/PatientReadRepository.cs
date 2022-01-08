using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class PatientReadRepository : ReadBaseRepository<int, Patient>, IPatientReadRepository
    {
        public PatientReadRepository(AppDbContext context) : base(context)
        {
        }

        public Patient GetPatient(string username)
        {
            return GetAll().Include(p => p.MedicalRecord).ThenInclude(mr => mr.Doctor)
                .Include(p => p.MedicalRecord).ThenInclude(mr => mr.Allergies)
                .ThenInclude(a => a.MedicationIngredient).FirstOrDefault(p => p.UserName.Equals(username));
        }

        public Patient GetByUsername(string username)
        {
            return GetAll().FirstOrDefault(p => p.UserName.Equals(username));
        }
    }
}
