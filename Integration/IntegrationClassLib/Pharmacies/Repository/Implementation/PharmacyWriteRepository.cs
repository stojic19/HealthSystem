using Integration.Database.EfStructures;
using Integration.Pharmacies.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacies.Repository.Implementation
{
    public class PharmacyWriteRepository : WriteBaseRepository<Pharmacy>, IPharmacyWriteRepository
    {
        public PharmacyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
