using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class DiagnoseWriteRepository : WriteBaseRepository<Diagnose>, IDiagnoseWriteRepository
    {
        public DiagnoseWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
