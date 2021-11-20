using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;


namespace Hospital.Repositories.DbImplementation
{
    public class AnsweredSurveyReadRepository : ReadBaseRepository<int, AnsweredSurvey>,IAnsweredSurveyReadRepository
    {
        public AnsweredSurveyReadRepository(AppDbContext context) :base(context)
        {

        }
    }
}
