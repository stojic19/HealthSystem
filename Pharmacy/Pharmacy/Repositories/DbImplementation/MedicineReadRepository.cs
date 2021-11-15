using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories.DbImplementation
{
    public class MedicineReadRepository : ReadBaseRepository<int, Medicine>, IMedicineReadRepository
    {
        public MedicineReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
