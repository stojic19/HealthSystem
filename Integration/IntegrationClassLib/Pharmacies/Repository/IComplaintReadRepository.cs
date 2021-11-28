using Integration.Pharmacies.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacies.Repository
{
    public interface IComplaintReadRepository : IReadBaseRepository<int, Complaint>
    {
    }
}
