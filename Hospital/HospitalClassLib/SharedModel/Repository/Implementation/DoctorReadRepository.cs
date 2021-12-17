using System.Collections.Generic;
using System.Linq;
using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;

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
                .GroupBy(t => new { Doctor = t.DoctorId })
                .Select(g => new { Count = g.Count(p => p.Id > 0), Doctor = g.Key.Doctor });

            var min = medRecords
                .GroupBy(t => t.DoctorId)
                .Select(g => g.Count()).Min();

            var retVal = query
                .Where(g => g.Count <= min + 2)
                .Select(g => g.Doctor).ToList();

            var doctors = _context.Set<Doctor>()
                .Where(d => d.Specialization.Name.ToLower().Equals("general practice")).AsEnumerable();

            return retVal.Select(id => doctors.FirstOrDefault(d => d.Id == id)).AsEnumerable();
        }

        public IEnumerable<Doctor> GetDoctorsBySpecialization(int? specializationId)
        {
            return GetAll().Include(x => x.Specialization).Where(x => x.SpecializationId == specializationId);
        }

        public IEnumerable<Doctor> GetAllDoctorsWithSpecialization()
        {
            return GetAll().Include(x => x.Specialization);
        }
        
        public IEnumerable<Doctor> GetSpecializedDoctors(int specializationId)
        {
            return _context.Set<Doctor>().Where(d => d.SpecializationId == specializationId);
        }
    }
}
