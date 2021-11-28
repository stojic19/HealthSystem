using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class AnsweredSurveyReadRepository : ReadBaseRepository<int, AnsweredSurvey>, IAnsweredSurveyReadRepository
    {
        public AnsweredSurveyReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
