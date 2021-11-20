using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class AnsweredQuestionReadRepository : ReadBaseRepository<int, AnsweredQuestion>, IAnsweredQuestionReadRepository
    {
        public AnsweredQuestionReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
