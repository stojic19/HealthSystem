using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class SpecializationWriteRepository : WriteBaseRepository<Specialization>, ISpecializationWriteRepository
    {
        public SpecializationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
