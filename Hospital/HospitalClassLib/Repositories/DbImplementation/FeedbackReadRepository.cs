using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class FeedbackReadRepository : ReadBaseRepository<int, Feedback>, IFeedbackReadRepository
    {
        public FeedbackReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
