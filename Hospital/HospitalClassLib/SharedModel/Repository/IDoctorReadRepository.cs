using System.Collections.Generic;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository
{
    public interface IDoctorReadRepository : IReadBaseRepository<int, Doctor>
    {
        public IEnumerable<Doctor> GetNonOverloadedDoctors();
        public IEnumerable<Doctor> GetSpecializedDoctors(int specializationId);
    }

}
