using Hospital.Database.EfStructures;
using Hospital.Shared_model.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Shared_model.Repository.Implementation
{
    public class SpecializationWriteRepository : WriteBaseRepository<Specialization>, ISpecializationWriteRepository
    {
        public SpecializationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
