using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class FeedbackWriteRepository : WriteBaseRepository<Feedback>, IFeedbackWriteRepository
    {
        public FeedbackWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
