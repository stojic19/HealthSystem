using System.Collections.Generic;
using System.Linq;
using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class DoctorReadRepository : ReadBaseRepository<int, Doctor>, IDoctorReadRepository
    {
        private readonly AppDbContext _context;
        public DoctorReadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Doctor> GetNonOverloadedDoctors()
        {
            var medRecords = _context.Set<MedicalRecord>().AsEnumerable();

            var query = medRecords
                .Where(x => !x.Patient.IsBlocked && x.Patient.EmailConfirmed && x.Doctor.Specialization.Name.ToLower().Equals("general practice"))
                .GroupBy(t => new { Doctor = t.DoctorId })
                .Select(g => new { Count = g.Count(p => p.Id > 0), Doctor = g.Key.Doctor });

            var min = medRecords
                .GroupBy(t => t.DoctorId)
                .Select(g => g.Count()).Min();

            var retVal = query
                .Where(g => g.Count <= min + 2)
                .Select(g => g.Doctor).ToList();

            var doctors = _context.Set<Doctor>().AsEnumerable();

            return retVal.Select(id => doctors.FirstOrDefault(d => d.Id == id)).AsEnumerable();
        }
    }
}
