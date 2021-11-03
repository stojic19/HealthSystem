using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class PharmacyWriteRepository : WriteBaseRepository<Pharmacy>, IPharmacyWriteRepository
    {
        public PharmacyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
