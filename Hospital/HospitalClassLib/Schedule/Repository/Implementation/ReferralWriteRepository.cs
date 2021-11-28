using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ReferralWriteRepository : WriteBaseRepository<Referral>, IReferralWriteRepository
    {
        public ReferralWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
