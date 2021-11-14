using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories.DbImplementation
{
    public class HospitalReadRepository : ReadBaseRepository<int, Hospital>, IHospitalReadRepository
    {
        public HospitalReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
