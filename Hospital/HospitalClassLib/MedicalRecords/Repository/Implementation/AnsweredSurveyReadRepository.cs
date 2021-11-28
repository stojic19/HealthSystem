using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class AnsweredSurveyReadRepository : ReadBaseRepository<int, AnsweredSurvey>, IAnsweredSurveyReadRepository
    {
        public AnsweredSurveyReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
