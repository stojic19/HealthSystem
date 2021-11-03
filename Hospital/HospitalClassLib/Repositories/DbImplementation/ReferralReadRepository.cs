using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class ReferralReadRepository : ReadBaseRepository<int, Referral>, IReferralReadRepository
    {
        public ReferralReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
