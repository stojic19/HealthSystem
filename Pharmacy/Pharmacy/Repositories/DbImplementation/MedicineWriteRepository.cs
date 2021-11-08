using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories.DbImplementation
{
    public class MedicineWriteRepository : WriteBaseRepository<Medicine>, IMedicineWriteRepository
    {
        public MedicineWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
