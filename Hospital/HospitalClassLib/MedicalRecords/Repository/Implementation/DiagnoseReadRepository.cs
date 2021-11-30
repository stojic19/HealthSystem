using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class DiagnoseReadRepository : ReadBaseRepository<int, Diagnose>, IDiagnoseReadRepository
    {
        public DiagnoseReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
