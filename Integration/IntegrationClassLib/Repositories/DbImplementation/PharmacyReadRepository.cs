using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class PharmacyReadRepository : ReadBaseRepository<int, Pharmacy>, IPharmacyReadRepository
    {
        public PharmacyReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
