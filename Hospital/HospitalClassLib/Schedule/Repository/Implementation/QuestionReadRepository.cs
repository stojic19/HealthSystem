using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class QuestionReadRepository : ReadBaseRepository<int, Question>, IQuestionReadRepository
    {
        public QuestionReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
