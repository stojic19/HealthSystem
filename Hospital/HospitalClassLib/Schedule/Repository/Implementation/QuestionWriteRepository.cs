using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class QuestionWriteRepository : WriteBaseRepository<Question>, IQuestionWriteRepository
    {
        public QuestionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
