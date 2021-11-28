using Hospital.Database.EfStructures;
using Hospital.Shared_model.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Shared_model.Repository.Implementation
{
    public class SpecializationReadRepository : ReadBaseRepository<int, Specialization>, ISpecializationReadRepository
    {
        public SpecializationReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
