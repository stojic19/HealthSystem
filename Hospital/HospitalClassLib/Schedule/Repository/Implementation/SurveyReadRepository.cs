using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class SurveyReadRepository : ReadBaseRepository<int, Survey>, ISurveyReadRepository
    {
        public SurveyReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
