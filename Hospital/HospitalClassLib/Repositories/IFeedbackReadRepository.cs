using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IFeedbackReadRepository : IReadBaseRepository<int, Feedback>
    {
    }
}
