using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class AnsweredQuestionWriteRepository : WriteBaseRepository<AnsweredQuestion>, IAnsweredQuestionWriteRepository
    {
        public AnsweredQuestionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
