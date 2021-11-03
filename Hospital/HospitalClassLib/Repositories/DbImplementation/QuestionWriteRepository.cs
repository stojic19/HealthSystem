using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class QuestionWriteRepository : WriteBaseRepository<Question>, IQuestionWriteRepository
    {
        public QuestionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
