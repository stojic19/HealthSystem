using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class AnsweredSurveyWriteRepository : WriteBaseRepository<AnsweredSurvey>, IAnsweredSurveyWriteRepository
    {
        public AnsweredSurveyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
