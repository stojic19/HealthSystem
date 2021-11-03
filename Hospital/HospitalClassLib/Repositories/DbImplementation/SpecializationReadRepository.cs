using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class SpecializationReadRepository : ReadBaseRepository<int, Specialization>, ISpecializationReadRepository
    {
        public SpecializationReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
