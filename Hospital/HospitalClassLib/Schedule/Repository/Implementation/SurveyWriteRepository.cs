using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class SurveyWriteRepository : WriteBaseRepository<Survey>, ISurveyWriteRepository
    {
        public SurveyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
