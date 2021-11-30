using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class SpecializationReadRepository : ReadBaseRepository<int, Specialization>, ISpecializationReadRepository
    {
        public SpecializationReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
