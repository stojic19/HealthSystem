using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class SurveyReadRepository : ReadBaseRepository<int, Survey>, ISurveyReadRepository
    {
        public SurveyReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
