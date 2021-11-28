using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class AnsweredQuestionWriteRepository : WriteBaseRepository<AnsweredQuestion>, IAnsweredQuestionWriteRepository
    {
        public AnsweredQuestionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
