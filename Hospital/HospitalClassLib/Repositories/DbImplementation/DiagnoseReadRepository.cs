using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class DiagnoseReadRepository : ReadBaseRepository<int, Diagnose>, IDiagnoseReadRepository
    {
        public DiagnoseReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
