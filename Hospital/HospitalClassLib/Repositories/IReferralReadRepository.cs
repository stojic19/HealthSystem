using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IReferralReadRepository : IReadBaseRepository<int, Referral>
    {
    }
}
