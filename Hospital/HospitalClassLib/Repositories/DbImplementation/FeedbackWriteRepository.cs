using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class FeedbackWriteRepository : WriteBaseRepository<Feedback>, IFeedbackWriteRepository
    {
        public FeedbackWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
