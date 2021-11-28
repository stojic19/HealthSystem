using Integration.Database.EfStructures;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacies.Repository.Implementation
{
    public class PharmacyWriteRepository : WriteBaseRepository<Model.Pharmacy>, IPharmacyWriteRepository
    {
        public PharmacyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
