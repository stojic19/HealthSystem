using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class AnsweredQuestionWriteRepository : WriteBaseRepository<AnsweredQuestion>, IAnsweredQuestionWriteRepository
    {
        public AnsweredQuestionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
