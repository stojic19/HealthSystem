using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class SurveyWriteRepository : WriteBaseRepository<Survey>, ISurveyWriteRepository
    {
        public SurveyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
