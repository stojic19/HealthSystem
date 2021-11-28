using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class DiagnoseWriteRepository : WriteBaseRepository<Diagnose>, IDiagnoseWriteRepository
    {
        public DiagnoseWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
