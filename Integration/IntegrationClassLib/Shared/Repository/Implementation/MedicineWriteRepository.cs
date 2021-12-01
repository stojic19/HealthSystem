using Integration.Database.EfStructures;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Repository.Implementation
{
    public class MedicineWriteRepository : WriteBaseRepository<Medicine>, IMedicineWriteRepository
    {
        public MedicineWriteRepository(AppDbContext context) : base(context)
        {

        }
    }
}
