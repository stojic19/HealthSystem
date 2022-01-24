using System;
using System.Collections.Generic;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository
{
    public interface IDoctorReadRepository : IReadBaseRepository<int, Doctor>
    {
        public IEnumerable<Doctor> GetNonOverloadedDoctors();
        public IEnumerable<Doctor> GetAllDoctorsWithSpecialization();
        public IEnumerable<Doctor> GetSpecializedDoctors(string specializationName);
        public IEnumerable<Specialization> GetAllSpecializations();
        bool IsDoctorAvailableInTerm(int doctorId, DateTime date);
        public Doctor GetDoctor(int id);
    }

}
