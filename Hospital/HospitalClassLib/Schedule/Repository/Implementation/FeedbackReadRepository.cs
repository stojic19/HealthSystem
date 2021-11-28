using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class FeedbackReadRepository : ReadBaseRepository<int, Feedback>, IFeedbackReadRepository
    {
        public FeedbackReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
