using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class SpecializationWriteRepository : WriteBaseRepository<Specialization>, ISpecializationWriteRepository
    {
        public SpecializationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
