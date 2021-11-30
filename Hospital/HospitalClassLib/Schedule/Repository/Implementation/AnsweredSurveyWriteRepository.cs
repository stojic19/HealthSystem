using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class AnsweredSurveyWriteRepository : WriteBaseRepository<AnsweredSurvey>, IAnsweredSurveyWriteRepository
    {
        public AnsweredSurveyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
