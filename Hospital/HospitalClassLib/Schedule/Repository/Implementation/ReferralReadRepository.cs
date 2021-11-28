using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ReferralReadRepository : ReadBaseRepository<int, Referral>, IReferralReadRepository
    {
        public ReferralReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
