using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class PatientReadRepository : ReadBaseRepository<int, Patient>, IPatientReadRepository
    {
        public PatientReadRepository(AppDbContext context) : base(context)
        {
        }

        public Patient GetPatient(int id)
        {
            return GetAll().Include(x => x.City).First(x=>x.Id==id);
        }
    }
}
