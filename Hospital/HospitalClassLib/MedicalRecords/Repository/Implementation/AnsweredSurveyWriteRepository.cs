using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class AnsweredSurveyWriteRepository : WriteBaseRepository<AnsweredSurvey>, IAnsweredSurveyWriteRepository
    {
        public AnsweredSurveyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
